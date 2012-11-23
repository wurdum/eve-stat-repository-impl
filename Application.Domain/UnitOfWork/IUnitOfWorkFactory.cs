namespace Application.Domain.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
        IUnitOfWork Create(IDataContext dataContext);
    }
}
