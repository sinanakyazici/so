using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SO.Domain.Auditing;

namespace SO.Domain
{
    public class AuditedAggregateRootEntityTypeConfiguration<T, TId> : AggregateRootEntityTypeConfiguration<T, TId> where T : AuditedAggregateRoot<TId>
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatorName).HasColumnName("creator_name");
            builder.Property(x => x.CreationTime).HasColumnName("creation_time");
            builder.Property(x => x.LastModifierName).HasColumnName("last_modifier_name");
            builder.Property(x => x.LastModificationTime).HasColumnName("last_modification_time");
            builder.Property(x => x.ValidFor).HasColumnName("valid_for");
            builder.HasQueryFilter(x => !x.ValidFor.HasValue || (x.ValidFor.HasValue && x.ValidFor.Value >= DateTime.Now));
            base.Configure(builder);
        }

        public static DateTime TurkeyDateTimeNow()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Asia/Kuwait"));
        }
    }
}