using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SO.Infrastructure.Data.EfCore
{
    public abstract class BaseDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggerFactory _loggerFactory;

        protected BaseDbContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _loggerFactory = loggerFactory;
        }


        private void UpdateCreationAudited(object entity)
        {
            entity.GetType().GetProperty("CreationTime")?.SetValue(entity, Helper.DateTimeNow());

            var creatorUser = _httpContextAccessor.HttpContext.GetCurrentUserId();
            var prop = entity.GetType().GetProperty("CreatorName");

            if (!string.IsNullOrWhiteSpace(creatorUser)) prop?.SetValue(entity, creatorUser);

            if (prop != null && string.IsNullOrWhiteSpace(prop.GetValue(entity)?.ToString())) throw new ArgumentException("Must CreatorName");
        }

        private void UpdateModificationAudited(object entity)
        {
            entity.GetType().GetProperty("LastModificationTime")?.SetValue(entity, Helper.DateTimeNow());
            var modiferUser = _httpContextAccessor.HttpContext.GetCurrentUserId();
            var prop = entity.GetType().GetProperty("LastModifierName");
            if (!string.IsNullOrWhiteSpace(modiferUser)) prop?.SetValue(entity, modiferUser);

            if (prop != null && string.IsNullOrWhiteSpace(prop.GetValue(entity)?.ToString())) throw new ArgumentException("Must LastModifierName");
        }

        private void UpdateValidationAudited(object entity)
        {
            entity.GetType().GetProperty("ValidFor")?.SetValue(entity, Helper.DateTimeNow());
        }

        public override void RemoveRange(IEnumerable<object> entities)
        {
            var enumerable = entities.ToList();
            foreach (var entity in enumerable) UpdateValidationAudited(entity);

            base.UpdateRange(enumerable);
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            foreach (var navigationEntry in Entry(entity).Navigations)
            {
                if (navigationEntry is CollectionEntry collectionEntry)
                {
                    if (collectionEntry.CurrentValue != null)
                        foreach (var dependentEntry in collectionEntry.CurrentValue)
                            UpdateValidationAudited(dependentEntry);
                }
                else
                {
                    var dependentEntry = navigationEntry.CurrentValue;
                    if (dependentEntry != null) UpdateValidationAudited(dependentEntry);
                }
            }

            UpdateValidationAudited(entity);
            return base.Update(entity);
        }

        public void DeleteRange(IEnumerable<object> entityList)
        {
            base.RemoveRange(entityList);
        }

        public EntityEntry<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Remove(entity);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("Command");
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging().UseNpgsql(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            var addedNewEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var ee in addedNewEntities) UpdateCreationAudited(ee.Entity);

            var updatedNewEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var ee in updatedNewEntities) UpdateModificationAudited(ee.Entity);

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var addedNewEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var ee in addedNewEntities) UpdateCreationAudited(ee.Entity);

            var updatedNewEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var ee in updatedNewEntities) UpdateModificationAudited(ee.Entity);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}