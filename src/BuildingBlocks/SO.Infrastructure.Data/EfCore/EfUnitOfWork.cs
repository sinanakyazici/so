using SO.Domain;

namespace SO.Infrastructure.Data.EfCore
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly BaseDbContext _baseDbContext;

        public EfUnitOfWork(BaseDbContext baseDbContext)
        {
            _baseDbContext = baseDbContext;
        }

        public IEnumerable<IEntity> GetEntities()
        {
            return _baseDbContext.ChangeTracker.Entries<IEntity>().Select(x => x.Entity);
        }

        public int Commit()
        {
            return _baseDbContext.SaveChanges();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _baseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}