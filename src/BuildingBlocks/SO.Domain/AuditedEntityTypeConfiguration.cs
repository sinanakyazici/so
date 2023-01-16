using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SO.Domain.Auditing;

namespace SO.Domain
{
    public class AuditedEntityTypeConfiguration<T, TId> : EntityTypeConfiguration<T, TId> where T : AuditedEntity<TId>
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatorName).HasColumnName("creator_name");
            builder.Property(x => x.CreationTime).HasColumnName("creation_time");
            builder.Property(x => x.LastModifierName).HasColumnName("last_modifier_name");
            builder.Property(x => x.LastModificationTime).HasColumnName("last_modification_time");
            builder.Property(x => x.ValidFor).HasColumnName("valid_for");
            base.Configure(builder);
        }
    }
}