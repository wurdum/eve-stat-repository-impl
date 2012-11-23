using System;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using Application.Domain.Domains;
using Application.Domain.UnitOfWork;

namespace Infrastructure.Database.Repositories
{
    public class ItemGroupsRepository : BaseRepository<ItemGroup, LinqMapper.ItemGroup>
    {
        #region inherited members

        public ItemGroupsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Table<LinqMapper.ItemGroup> DbTable
        {
            get { return UnitOfWork.DataContext.GetTable<LinqMapper.ItemGroup>(); }
        }

        protected override void MapEntry(LinqMapper.ItemGroup dbEntity, ItemGroup domainEntity)
        {
            dbEntity.IDRootGroup = domainEntity.IdRootGoup;
            dbEntity.Name = domainEntity.Name;
        }

        #endregion
    }
}
