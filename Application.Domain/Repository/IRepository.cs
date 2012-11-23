namespace Application.Domain.Repository
{
    public interface IRepository<TEntity> 
        where TEntity : BaseDomainEntity
    {
        bool SaveOnCommit(TEntity domainEntity);
        bool DeleteOnCommit(TEntity entity);
    }
}
