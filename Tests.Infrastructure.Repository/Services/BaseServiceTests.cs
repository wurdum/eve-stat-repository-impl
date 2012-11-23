using Application.Domain.Services;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;
using Infrastructure.Database.Services;
using Infrastructure.Database.UnitOfWork;
using Moq;
using Xunit;

namespace Tests.Infrastructure.Repository.Services
{
    public class BaseServiceTests
    {
        private DmDataContext ActualDataContext
        {
            get { return new DmDataContext(); }
        }

        [Fact]
        public void DefaultCtorWillUsedCustomDataContext()
        {
            IUserService userService = new UserService();

            Assert.IsType<CustomDataContext>(userService.DataContext);
        }

        [Fact]
        public void CtorWithDataContextReassignDataContextProp()
        {
            var dataContext = new Mock<IDataContext>();
            IUserService userService = new UserService(dataContext.Object);

            Assert.Equal(dataContext.Object, userService.DataContext);
        }

        [Fact]
        public void GetAllMethodExtractedDataFromDataContext()
        {
            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(dc => dc.GetTable<Administrator>())
                .Returns(ActualDataContext.Administrators);
            IUserService userService = new UserService(dataContext.Object);

            userService.GetAll();

            dataContext.Verify(dc => dc.GetTable<Administrator>(), Times.Once());
        }
    }
}