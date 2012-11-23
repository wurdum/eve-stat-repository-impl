using Application.Domain.UnitOfWork;
using Moq;
using Xunit;

namespace Tests.Infrastructure.Repository.UOW
{
    public class TestsUnitOfWork
    {
        [Fact]
        public void CheckThatCommitMethodWasCalled()
        {
            var dataContext = new Mock<IDataContext>();
            var uow = new global::Infrastructure.Database.UnitOfWork.UnitOfWork(dataContext.Object);

            uow.Commit();

            dataContext.Verify(dc => dc.Commit(), Times.Once());
        }

        [Fact]
        public void CheckThatDisposeMethodOfUowCalledDisposeOfDataContext()
        {
            var dataContext = new Mock<IDataContext>();
            var uow = new global::Infrastructure.Database.UnitOfWork.UnitOfWork(dataContext.Object);

            uow.Dispose();

            dataContext.Verify(dc => dc.Dispose(), Times.Once());
        }
    }
}
