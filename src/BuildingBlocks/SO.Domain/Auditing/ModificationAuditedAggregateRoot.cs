namespace SO.Domain.Auditing;

public abstract class ModificationAuditedAggregateRoot<TId> : CreationAuditedAggregateRoot<TId>
{
    public virtual DateTime? LastModificationTime { get; set; }
    public virtual string? LastModifierName { get; set; }
}
