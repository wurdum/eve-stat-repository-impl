using Application.Domain.Domains;
using Application.Domain.Services;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;
using Infrastructure.Database.Services;
using Moq;
using Xunit;

namespace Tests.Infrastructure.Repository.Services
{
    public class UserServiceTests
    {
        private DmDataContext ActualDataContext
        {
            get { return new DmDataContext(); }
        }

        [Fact]
        public void IsValidIfUserNameOrPassEmptyReturnsFalse()
        {
            IUserService userService = new UserService();
            var user = new User {  Password = "asd" };
            
            Assert.False(userService.IsValid(user));

            user.UserName = "asd";
            user.Password = string.Empty;

            Assert.False(userService.IsValid(user));
        }

        [Fact]
        public void GetByNameMethodMustExtractDataFromDataContext()
        {
            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(x => x.GetTable<Administrator>())
                .Returns(ActualDataContext.Administrators);
            IUserService userService = new UserService(dataContext.Object);

            userService.GetByName("someName");

            dataContext.Verify(dc => dc.GetTable<Administrator>(), Times.Once());
        }

        [Fact]
        public void GetByNameReturnsNullIfUserNotFinded()
        {
            IUserService userService = new UserService();

            var user = userService.GetByName("asdasdasd3423sdf");

            Assert.Null(user);

            user = userService.GetByName("");

            Assert.Null(user);
        }
    }
}