using Application.Domain;
using Application.Domain.Domains;
using Xunit;

namespace Tests.Application.Domain
{
    public class TestsItemGroup
    {
        [Fact]
        public void TestHashCode()
        {
            var item = new ItemGroup { ID = 1, Name = "1" };
            var item2 = new ItemGroup { ID = 1, Name = "1" };

            Assert.Equal(item.GetHashCode(), item2.GetHashCode());
        }

        [Fact]
        public void TestEqualWithSelfAndWithNull()
        {
            var item = new ItemGroup { ID = 1, Name = "1" };
            var item2 = new ItemGroup { ID = 1, Name = "1" };

            Assert.True(item.Equals(item));
            Assert.False(item.Equals(null));
            Assert.True(item.Equals(item2));
        }

        [Fact]
        public void TestEqualWithObject()
        {
            var item = new ItemGroup { ID = 1, Name = "1" };
            var item2 = new ItemGroup { ID = 1, Name = "1" };
            var itemGroup = new Item();

            Assert.False(item.Equals((object)null));
            Assert.True(item.Equals((object)item));
            Assert.False(item.Equals((object)itemGroup));
            Assert.True(item.Equals(item2));
        }
    }
}