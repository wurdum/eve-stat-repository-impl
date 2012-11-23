using System.Collections.Generic;
using Application.Domain.UnitOfWork;

namespace Application.Domain.Services
{
    public interface IService<T> where T : BaseDomainEntity
    {
        IDataContext DataContext { get; }
        IEnumerable<T> GetAll();
    }
}