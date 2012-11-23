using Application.Domain.Domains;

namespace Application.Domain.Services
{
    public interface IItemService : IService<Item>
    {
        int GetId(Item item);
    }
}