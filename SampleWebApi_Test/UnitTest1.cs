using SampleWebApi._3.Services;
using SampleWebApi_Test.Moq;
using System.Collections.Immutable;

namespace SampleWebApi_Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }

    public class ItemServiceTest
    {
        ItemRepositoryMoq _itemRepository;
        public ItemServiceTest()
        {
             _itemRepository = new ItemRepositoryMoq();
        }
        [Fact]
        public async void GetSimilarItem_FindItemId_MustReturn3Items()
        {
            //Arrange
            var itemRepository = new ItemRepositoryMoq();
            var service = new ItemService(itemRepository);

            //act
            var result=await service.GetSimilarItemByPriceAsync(itemRepository._itemId + "5", 3, 0.05M);

            //assert
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            Assert.Equal(itemRepository._itemId + "9", result[0].Id);
            Assert.Equal(itemRepository._itemId + "8", result[1].Id);
            Assert.Equal(itemRepository._itemId + "7", result[2].Id);

        }

        [Fact]
        public async void GetSimilarItem_NotFindItemId_MustBeNull()
        {
            //Arrange
            var service = new ItemService(_itemRepository);

            //act
            var result = await service.GetSimilarItemByPriceAsync(_itemRepository._itemId + "50", 3, 0.05M);

            //assert
            Assert.Null(result);
            

        }
        [Fact]
        public async void GetSimilarItem_SmallDeviation_DoesNotContainItself()
        {
            //Arrange
            var service = new ItemService(_itemRepository);

            //act
            var result = await service.GetSimilarItemByPriceAsync(_itemRepository._itemId + "5", 3, 0.00001M);

            //assert
            Assert.NotNull(result);
            Assert.True(result.Any(a=>a.Id== _itemRepository._itemId + "5")==false);

        }

        public async void GetSimilarItem_NullItemId_MustBeNull()
        {
            //Arrange
            var service = new ItemService(_itemRepository);

            //act
            var result = await service.GetSimilarItemByPriceAsync(null, 3, 0.00001M);

            //assert
            Assert.Null(result);

        }
    }
}