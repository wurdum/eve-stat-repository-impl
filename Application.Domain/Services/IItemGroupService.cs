using Application.Domain.Domains;

namespace Application.Domain.Services
{
    public interface IItemGroupService:IService<ItemGroup>
    {
        int GetId(ItemGroup item);
    }
}