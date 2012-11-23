using System;

namespace Application.Domain.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IDataContext DataContext { get; }
        void Commit();
    }
}
