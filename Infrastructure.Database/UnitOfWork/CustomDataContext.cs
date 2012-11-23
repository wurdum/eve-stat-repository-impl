using System;
using System.Data.Linq;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;

namespace Infrastructure.Database.UnitOfWork
{
    public class CustomDataContext : IDataContext
    {
        private readonly DmDataContext _dataContext;
        private bool _disposed;

        public CustomDataContext()
        {
            _dataContext = new DmDataContext();
        }
        
        public void Commit()
        {
            _dataContext.SubmitChanges();
        }

        public Table<TEntity> GetTable<TEntity>() where TEntity : class
        {
            return _dataContext.GetTable<TEntity>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispising)
        {
            if (_disposed)
            {
                if (dispising)
                {
                    _dataContext.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
