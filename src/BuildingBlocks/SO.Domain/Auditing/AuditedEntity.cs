namespace SO.Domain.Auditing
{
    public abstract class AuditedEntity<TId> : CreationAuditedEntity<TId>
    {
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual string? LastModifierName { get; set; }
    }
}