using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SO.Domain
{
    public class AggregateRootEntityTypeConfiguration<T, TId> : EntityTypeConfiguration<T, TId> where T : AggregateRoot<TId>
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Ignore(x => x.DomainEvents);
            base.Configure(builder);
        }
    }
}