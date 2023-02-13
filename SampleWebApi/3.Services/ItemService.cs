using SampleWebApi._2.Database.Repositories;
using SampleWebApi._4.ViewModels.Items;

namespace SampleWebApi._3.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<SimilarItemViewModel>> GetSimilarItemByPriceAsync(string itemId, int count = 3, decimal deviation = 0.01M)
        {
            if (string.IsNullOrEmpty(itemId))
                return null;

            var item = await _itemRepository.GetByIdAsync(itemId);

            if (item is null)
                return null;

            var price = item.GetPrice(DateTime.Now).Price;
            decimal minPrice = price - price * deviation;
            decimal maxPrice = price + price * deviation;

            var result = await _itemRepository.GetItemListByPriceRangeAsync(minPrice, maxPrice, count);
            if(result is not null)
            {
                result.RemoveAll(a => a.Id == itemId);
            }

            return result;
        }
    }
}
