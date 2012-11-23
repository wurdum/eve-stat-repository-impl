using Application.Domain.Repository;
using Application.Domain.Services;
using Application.Domain.UnitOfWork;
using Infrastructure.Database.LinqMapper;
using Infrastructure.Database.Repositories;
using Infrastructure.Database.Services;
using Infrastructure.Database.UnitOfWork;
using Moq;
using Xunit;
using System.Linq;

namespace Tests.Infrastructure.Repository.Repositories
{
    public class TestsItemsRepository
    {
        private Mock<IDataContext> _dataContext = new Mock<IDataContext>();
        private IUnitOfWork _unitOfWork;
        private IRepository<Application.Domain.Domains.Item> _repository;

        private static IItemService ItemService
        {
            get { return new ItemService(); }
        }

        private void PrepareRepositoryWithFictionDataContext()
        {
            _dataContext.Setup(dc => dc.GetTable<Item>())
                .Returns(new DmDataContext().Items);
            _dataContext.Setup(dc => dc.Commit());

            _unitOfWork = new UnitOfWork(_dataContext.Object);
            _repository = new ItemsRepository(_unitOfWork);
        }

        private void PrepareRepository()
        {
            var unitOfWorkFactory = new UnitOfWorkFactory();
            _unitOfWork = unitOfWorkFactory.Create();
            _repository = new ItemsRepository(_unitOfWork);
        }

        private static Application.Domain.Domains.Item GetNewItem()
        {
            const int itemIdGroup = 9; // MAGIC NUMBER. Id must exist in database.
            const string itemName = "Test item";
            const string itemDescription = "Some description";
            const double itemVolume = 0.15;
            const string itemImage = "someFunnyImage.jpg";

            var newItem = new Application.Domain.Domains.Item
                              {
                                  IdItemGroup = itemIdGroup,
                                  Name = itemName,
                                  Description = itemDescription,
                                  Volume = itemVolume,
                                  Image = itemImage
                              };

            return newItem;
        }

        [Fact]
        public void SaveOnCommitMustReturnedTrueAndCalledInsertOnSubmit()
        {
            PrepareRepositoryWithFictionDataContext();
            var newItem = new Application.Domain.Domains.Item();

            var result = _repository.SaveOnCommit(newItem);

            Assert.True(result);
            _dataContext.Verify(dc => dc.GetTable<Item>(), Times.Once());
        }

        [Fact]
        public void TestThatAfterSaveNewItemIdChanged()
        {
            PrepareRepository();
            var newItem = new Application.Domain.Domains.Item();
            Assert.True(newItem.ID == -1);

            var result = _repository.SaveOnCommit(newItem);

            Assert.True(result);
            Assert.True(newItem.ID != -1);
        }

        [Fact]
        public void TestThatItemWithId0NotNewButCannotBeUpdated()
        {
            PrepareRepository();
            var newItem = new Application.Domain.Domains.Item {ID = 0};

            var result = _repository.SaveOnCommit(newItem);

            Assert.False(result);
        }

        [Fact]
        public void TestDeleteNotExistItem()
        {
            PrepareRepository();
            var newItem = new Application.Domain.Domains.Item {ID = 0};

            var result = _repository.DeleteOnCommit(newItem);

            Assert.False(result);
        }

        [Fact]
        public void MainTestAddNewItemGetItAndThenDeleteIt()
        {
            PrepareRepository();
            var newItem = GetNewItem();
            Assert.True(newItem.ID == -1);

            var result = _repository.SaveOnCommit(newItem);
            Assert.True(result);
            Assert.True(newItem.ID != -1);

            _unitOfWork.Commit();

            newItem.ID = ItemService.GetId(newItem);

            var receivedItem = ItemService.GetAll().Where(i => i.ID.Equals(newItem.ID)).SingleOrDefault();
            Assert.True(receivedItem.Equals(newItem));

            receivedItem.Name = "Test item 2";
            result = _repository.SaveOnCommit(receivedItem);
            Assert.True(result);

            _unitOfWork.Commit();

            receivedItem = ItemService.GetAll().Where(i => i.ID.Equals(newItem.ID)).SingleOrDefault();
            Assert.False(receivedItem.Equals(newItem));
            Assert.True(receivedItem.ID == newItem.ID);

            result = _repository.DeleteOnCommit(receivedItem);
            Assert.True(result);

            _unitOfWork.Commit();
        }
    }
}