namespace SO.Domain.Auditing
{
    public abstract class CreationAuditedAggregateRoot<TId> : ValidationAuditedAggregateRoot<TId>
    {
        public virtual DateTime CreationTime { get; set; } = DateTime.Now;
        public virtual string CreatorName { get; set; } = null!;
    }
}