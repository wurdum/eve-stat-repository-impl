using System;
using System.Linq;
using System.Linq.Expressions;
using Application.Domain.Domains;
using Application.Domain.Services;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;

namespace Infrastructure.Database.Services
{
    public class UserService : BaseService<User, Administrator>, IUserService
    {
        public UserService()
        {
        }

        public UserService(IDataContext dataContext) : base(dataContext)
        {
        }

        public bool IsValid(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) ||
                string.IsNullOrEmpty(user.Password))
                return false;

            var administrator = GetAllWithName(user.UserName).SingleOrDefault();

            if (administrator == null)
                return false;

            return CompareUsers(user, administrator);
        }

        public User GetByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            return DataContext.GetTable<Administrator>()
                .Where(a => a.UserName.Equals(userName)).Select(GetConverter()).SingleOrDefault();
        }

        protected override Expression<Func<Administrator, User>> GetConverter()
        {
            return x => new User
                            {
                                ID = x.ID,
                                UserName = x.UserName,
                                Email = x.Email
                            };
        }

        private bool CompareUsers(User user, Administrator administrator)
        {
            var userHashedPass = User.GenerateSaltedHash(user.Password, administrator.Salt);

            return user.UserName == administrator.UserName &&
                   userHashedPass == administrator.Password;
        }

        private IQueryable<Administrator> GetAllWithName(string userName)
        {
            return DataContext.GetTable<Administrator>()
                .Where(a => a.UserName.Equals(userName));
        }
    }
}