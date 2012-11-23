using Application.Domain.Domains;

namespace Application.Domain.Services
{
    public interface IUserService : IService<User>
    {
        bool IsValid(User user);
        User GetByName(string userName);
    }
}