using System;
using System.Data.Linq;
using System.Linq;
using Application.Domain.Domains;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;

namespace Infrastructure.Database.Repositories
{
    public class UsersRepository : BaseRepository<User, Administrator>
    {
        public UsersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Administrator FindDbEntity(User domainEntity)
        {
            return DbTable.Where(x => x.UserName.Equals(domainEntity.UserName)).SingleOrDefault();
        }

        protected override Table<Administrator> DbTable
        {
            get { return UnitOfWork.DataContext.GetTable<Administrator>(); }
        }

        protected override void MapEntry(Administrator dbEntity, User domainEntity)
        {
            if (string.IsNullOrEmpty(domainEntity.UserName) ||
                string.IsNullOrEmpty(domainEntity.Password))
                throw new ArgumentException("UserName or Password is empty.");

            var passwordSalt = User.GenerateSalt();
            var hashedPassword = User.GenerateSaltedHash(domainEntity.Password, passwordSalt);

            dbEntity.UserName = domainEntity.UserName;
            dbEntity.Password = hashedPassword;
            dbEntity.Salt = passwordSalt;
            dbEntity.Email = domainEntity.Email;
        }
    }
}