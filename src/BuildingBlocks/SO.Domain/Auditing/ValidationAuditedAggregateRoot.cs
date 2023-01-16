namespace SO.Domain.Auditing
{
    public abstract class ValidationAuditedAggregateRoot<TId> : AggregateRoot<TId>
    {
        public virtual DateTime? ValidFor { get; set; }
    }
}