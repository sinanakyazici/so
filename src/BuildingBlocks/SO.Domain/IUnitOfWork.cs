namespace SO.Domain
{
    public interface IUnitOfWork
    {
        IEnumerable<IEntity> GetEntities();
        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}