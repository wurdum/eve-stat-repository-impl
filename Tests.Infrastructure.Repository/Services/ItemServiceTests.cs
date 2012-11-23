using System;
using Application.Domain.Services;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;
using Infrastructure.Database.Services;
using Moq;
using Xunit;

namespace Tests.Infrastructure.Repository.Services
{
    public class ItemServiceTests
    {
        private Application.Domain.Domains.Item GetNewItem()
        {
            const int itemIdGroup = 9; // MAGIC NUMBER. Id must exist in database.
            const string itemName = "Test item";
            const string itemDescription = "Some description";
            const double itemVolume = 0.15;
            const string itemImage = "someFunnyImage.jpg";

            return new Application.Domain.Domains.Item
                       {
                           IdItemGroup = itemIdGroup,
                           Name = itemName,
                           Description = itemDescription,
                           Volume = itemVolume,
                           Image = itemImage
                       };
        }

        private DmDataContext ActualDataContext
        {
            get { return new DmDataContext(); }
        }

        [Fact]
        public void GetIdIfItemIsNullThrowException()
        {
            IItemService itemService = new ItemService();

            Assert.Throws<ArgumentNullException>(() => { itemService.GetId(null); });
        }

        [Fact]
        public void GetIdMethodMustExtractDataFromDataContext()
        {
            var dataContext = new Mock<IDataContext>();
            dataContext.Setup(x => x.GetTable<Item>())
                .Returns(ActualDataContext.Items);
            IItemService userService = new ItemService(dataContext.Object);

            var newItem = GetNewItem();

            userService.GetId(newItem);

            dataContext.Verify(dc => dc.GetTable<Item>(),
                Times.Once());
        }

        [Fact]
        public void GetIdIfItemDontFindedReturns0()
        {
            IItemService itemService = new ItemService();

            var newItem = GetNewItem();

            var id = itemService.GetId(newItem);

            Assert.Equal(0, id);
        }
    }
}