using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SO.Domain.Auditing;

namespace SO.Domain
{
    public class CreationAuditedAggregateRootEntityTypeConfiguration<T, TId> : AggregateRootEntityTypeConfiguration<T, TId> where T : CreationAuditedAggregateRoot<TId>
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatorName).HasColumnName("creator_name");
            builder.Property(x => x.CreationTime).HasColumnName("creation_time");
            builder.Property(x => x.ValidFor).HasColumnName("valid_for");
            builder.HasQueryFilter(x => !x.ValidFor.HasValue || (x.ValidFor.HasValue && x.ValidFor.Value >= DateTime.Now));
            base.Configure(builder);
        }
    }
}