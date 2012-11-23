using System;
using System.Linq;
using System.Linq.Expressions;
using Application.Domain.Domains;
using Application.Domain.Services;

namespace Infrastructure.Database.Services
{
    public class ItemGroupService : BaseService<ItemGroup, LinqMapper.ItemGroup>, IItemGroupService
    {
        protected override Expression<Func<LinqMapper.ItemGroup, ItemGroup>> GetConverter()
        {
            return i => new ItemGroup
                            {
                                ID = i.ID,
                                IdRootGoup = i.IDRootGroup,
                                Name = i.Name
                            };
        }

        public int GetId(ItemGroup itemGroup)
        {
            var result = DataContext.GetTable<LinqMapper.ItemGroup>().Where(e =>
                e.IDRootGroup.Equals(itemGroup.IdRootGoup) &&
                e.Name.Equals(itemGroup.Name)).SingleOrDefault();

            return result == null ? 0 : result.ID;
        }
    }
}