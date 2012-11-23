using System;
using System.Linq;
using System.Linq.Expressions;
using Application.Domain.Domains;
using Application.Domain.Services;
using Application.Domain.UnitOfWork;

namespace Infrastructure.Database.Services
{
    public class ItemService : BaseService<Item, LinqMapper.Item>, IItemService
    {
        public ItemService()
        {
        }

        public ItemService(IDataContext dataContext) : base(dataContext)
        {
        }

        protected override Expression<Func<LinqMapper.Item, Item>> GetConverter()
        {
            return x => new Item
                            {
                                ID = x.ID,
                                Name = x.Name,
                                IdItemGroup = x.IDItemGroups,
                                Description = x.Description,
                                Image = x.Image,
                                Volume = x.Volume
                            };
        }

        public int GetId(Item item)
        {
            var result = DataContext.GetTable<LinqMapper.ItemGroup>().Where(e =>
                e.IDRootGroup.Equals(item.IdItemGroup) &&
                e.Name.Equals(item.Name)).SingleOrDefault();

            return result == null ? 0 : result.ID;
        }
    }
}