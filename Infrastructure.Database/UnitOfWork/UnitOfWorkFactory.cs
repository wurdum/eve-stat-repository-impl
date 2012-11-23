using System;
using Application.Domain.UnitOfWork;

namespace Infrastructure.Database.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public UnitOfWorkFactory()
        {
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork();
        }

        public IUnitOfWork Create(IDataContext dataContext)
        {
            return new UnitOfWork(dataContext);
        }
    }
}