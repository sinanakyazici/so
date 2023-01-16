namespace SO.Domain.Auditing
{
    public abstract class ValidationEntity<TId> : Entity<TId>
    {
        public virtual DateTime? ValidFor { get; set; }
    }
}