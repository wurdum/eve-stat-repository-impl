using Application.Domain.Domains;
using Xunit;

namespace Tests.Application.Domain
{
    public class TestsUser
    {
        [Fact]
        public void TestHashCode()
        {
            var item = new User {UserName = "aa", Password = "bb"};
            var item2 = new User {UserName = "aa", Password = "bb"};

            Assert.Equal(item.GetHashCode(), item2.GetHashCode());
        }

        [Fact]
        public void TestEqualWithSelfAndWithNull()
        {
            var item = new User { UserName = "aa", Password = "bb" };
            var item2 = new User {UserName = "aa", Password = "bb"};

            Assert.True(item.Equals(item));
            Assert.False(item.Equals(null));
            Assert.True(item.Equals(item2));
        }

        [Fact]
        public void TestEqualWithObject()
        {
            var item = new User { UserName = "aa", Password = "bb" };
            var item2 = new User { UserName = "aa", Password = "bb" };
            var itemGroup = new ItemGroup();

            Assert.False(item.Equals((object)null));
            Assert.True(item.Equals((object)item));
            Assert.False(item.Equals((object)itemGroup));
            Assert.True(item.Equals(item2));
        }

        [Fact]
        public void SaltAlwaysMystBeVarious()
        {
            Assert.False(User.GenerateSalt()
                .Equals(User.GenerateSalt()));
        }

        [Fact]
        public void GeneratedSaltedHashMustBeAlwaysEqual()
        {
            string pass = "1asfdgfdfs9";
            string salt = User.GenerateSalt();

            Assert.NotEmpty(salt);

            string hashedPassFirst = User.GenerateSaltedHash(pass, salt);
            string hashedPassSecond = User.GenerateSaltedHash(pass, salt);

            Assert.Equal(hashedPassFirst, hashedPassSecond);
        }
    }
}