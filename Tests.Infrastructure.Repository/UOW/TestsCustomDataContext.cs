using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;
using Infrastructure.Database.UnitOfWork;
using Xunit;

namespace Tests.Infrastructure.Repository.UOW
{
    public class TestsCustomDataContext
    {
        [Fact]
        public void TestThatDcReturnedCorrectTables()
        {
            IDataContext dataContext = new CustomDataContext();
            var tableFromDc = dataContext.GetTable<Item>();

            var dmDataContext = new DmDataContext();
            var tableFromDataMapper = dmDataContext.GetTable<Item>();

            Assert.True(tableFromDc.ToString().Equals(tableFromDataMapper.ToString()));
        }
    }
}