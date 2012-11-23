using Application.Domain.UnitOfWork;
using Infrastructure.Database.UnitOfWork;
using Xunit;

namespace Tests.Infrastructure.Repository.UOW
{
    public class TestsUnitOfWorkFactory
    {
        [Fact]
        public void FactoryMustReturnIUnitOfWorkIstance()
        {
            var factory = new UnitOfWorkFactory();

            var uow = factory.Create();

            Assert.IsType(typeof(UnitOfWork), uow);
        }

        [Fact]
        public void FactoryMustCreateUowWithDataContext()
        {
            IDataContext dataContext = new CustomDataContext();
            IUnitOfWorkFactory factory = new UnitOfWorkFactory();

            var uow = factory.Create(dataContext);
            
            Assert.Equal(dataContext, uow.DataContext);
        }

        [Fact]
        public void FactoryDefaultCtorCreateUowWithCustomDataContext()
        {
            IUnitOfWorkFactory unitOfWorkFactory = new UnitOfWorkFactory();

            var unitOfWork = unitOfWorkFactory.Create();

            Assert.IsType<CustomDataContext>(unitOfWork.DataContext);
        }
    }
}
