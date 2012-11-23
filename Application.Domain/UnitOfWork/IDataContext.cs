using System;
using System.Data.Linq;

namespace Application.Domain.UnitOfWork
{
    public interface IDataContext:IDisposable
    {
        void Commit();
        Table<TEntity> GetTable<TEntity>() where TEntity : class;
    }
}
