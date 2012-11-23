using System;
using Application.Domain.UnitOfWork;

namespace Infrastructure.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext _dataContext;

        public IDataContext DataContext
        {
            get { return _dataContext; }
        }

        public UnitOfWork() : this(new CustomDataContext())
        {
        }

        public UnitOfWork(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Commit()
        {
            _dataContext.Commit();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}