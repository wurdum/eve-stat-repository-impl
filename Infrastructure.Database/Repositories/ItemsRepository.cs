using System;
using System.Data.Linq;
using Application.Domain.Domains;
using Application.Domain.UnitOfWork;

namespace Infrastructure.Database.Repositories
{
    public class ItemsRepository : BaseRepository<Item, LinqMapper.Item>
    {
        #region inherited members

        public ItemsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Table<LinqMapper.Item> DbTable
        {
            get { return UnitOfWork.DataContext.GetTable<LinqMapper.Item>(); }
        }

        protected override void MapEntry(LinqMapper.Item dbEntity, Item domainEntity)
        {
            dbEntity.IDItemGroups = domainEntity.IdItemGroup;
            dbEntity.Name = domainEntity.Name;
            dbEntity.Description = domainEntity.Description;
            dbEntity.Volume = domainEntity.Volume;
            dbEntity.Image = domainEntity.Image;
        }

        #endregion
    }
}