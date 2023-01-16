namespace SO.Domain.Auditing
{
    public abstract class CreationAuditedEntity<TId> : ValidationEntity<TId>
    {
        public virtual DateTime CreationTime { get; set; } = DateTime.Now;
        public virtual string CreatorName { get; set; } = null!;
    }
}