using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.Domain;
using Application.Domain.Services;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;
using Infrastructure.Database.UnitOfWork;

namespace Infrastructure.Database.Services
{
    public abstract class BaseService<Tm, TDb>: IService<Tm> 
        where Tm : BaseDomainEntity
        where TDb : class, IDbEntity 
    {
        private readonly IDataContext _dataContext;

        public IDataContext DataContext
        {
            get { return _dataContext; }
        }

        protected BaseService() : this(new CustomDataContext())
        {
        }

        protected BaseService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Tm> GetAll()
        {
            return DataContext.GetTable<TDb>().Select(GetConverter());
        }

        protected abstract Expression<Func<TDb, Tm>> GetConverter();
    }
}