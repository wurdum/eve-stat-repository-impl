using System;
using Application.Domain;
using Application.Domain.Domains;
using Application.Domain.Repository;
using Application.Domain.Services;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.Repositories;
using Infrastructure.Database.Services;
using Infrastructure.Database.UnitOfWork;
using Xunit;

namespace Tests.Infrastructure.Repository.Repositories
{
    public class TestsUsersRepository
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<User> _repository;

        private static IUserService UserService
        {
            get { return new UserService(); }
        }

        private void PrepareRepository()
        {
            var unitOfWorkFactory = new UnitOfWorkFactory();
            _unitOfWork = unitOfWorkFactory.Create();
            _repository = new UsersRepository(_unitOfWork);
        }

        [Fact]
        public void TestUserCreationAdnDeleting()
        {
            PrepareRepository();

            var user = new User
                           {
                               UserName = "paasdasdva",
                               Password = "asdasdasdasdad",
                               Email = "superhomek@gmail.com"
                           };

            var result = _repository.SaveOnCommit(user);
            Assert.True(result);

            _unitOfWork.Commit();

            result = _repository.DeleteOnCommit(user);
            Assert.True(result);

            _unitOfWork.Commit();
        }

        [Fact]
        public void CreationNullUserMustFiredArgNullExceptio()
        {
            PrepareRepository();

            User user = null;

            Assert.Throws<ArgumentNullException>(() => _repository.SaveOnCommit(user));
        }

        [Fact]
        public void CreationUserWithoutNameAndPassFiredException()
        {
            PrepareRepository();

            var user = new User
                           {
                               Password = "sad",
                               Email = "superhomek@gmail.com"
                           };

            Assert.Throws<ArgumentException>(() => _repository.SaveOnCommit(user));

            user.UserName = "sad";
            user.Password = null;
            Assert.Throws<ArgumentException>(() => _repository.SaveOnCommit(user));
        }

        [Fact]
        public void UpdatingUserTests()
        {
            PrepareRepository();

            var user = new User{UserName = "as4234asdd", Password = "asd", Email = "asd"};

            var result = _repository.SaveOnCommit(user);
            Assert.True(result);

            _unitOfWork.Commit();

            var oldUser = UserService.GetByName(user.UserName);
            Assert.True(oldUser.ID != 0 && oldUser.ID != -1);

            oldUser.Password = "dsa";
            result = _repository.SaveOnCommit(oldUser);
            Assert.True(result);

            _unitOfWork.Commit();

            result = _repository.DeleteOnCommit(oldUser);
            Assert.True(result);

            _unitOfWork.Commit();
        }

        [Fact]
        public void UpdatingUnexistableItemIsImmposible()
        {
            PrepareRepository();

            var user = new User{ID = 300};

            var result = _repository.SaveOnCommit(user);

            Assert.False(result);
        }

        [Fact]
        public void UnexistableUserCanNotBeDeleted()
        {
            PrepareRepository();

            Assert.False(_repository.DeleteOnCommit(new User {UserName = "asdasd231asd"}));
        }
    }
}