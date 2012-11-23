using Application.Domain;
using Application.Domain.Domains;
using Xunit;

namespace Tests.Application.Domain
{
    public class TestsItem
    {
        [Fact]
        public void TestHashCode()
        {
            var item = new Item { ID = 1, Name = "1", Description = "2" };
            var item2 = new Item { ID = 1, Name = "1", Description = "2" };

            Assert.Equal(item.GetHashCode(), item2.GetHashCode());
        }

        [Fact]
        public void TestEqualWithSelfAndWithNull()
        {
            var item = new Item { ID = 1, Name = "1", Description = "2" };
            var item2 = new Item { ID = 1, Name = "1", Description = "2" };

            Assert.True(item.Equals(item));
            Assert.False(item.Equals(null));
            Assert.True(item.Equals(item2));
        }

        [Fact]
        public void TestEqualWithObject()
        {
            var item = new Item { ID = 1, Name = "1", Description = "2" };
            var item2 = new Item { ID = 1, Name = "1", Description = "2" };
            var itemGroup = new ItemGroup();

            Assert.False(item.Equals((object) null));
            Assert.True(item.Equals((object) item));
            Assert.False(item.Equals((object) itemGroup));
            Assert.True(item.Equals(item2));
        }
    }
}