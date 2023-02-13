using SampleWebApi._1.Entities;
using SampleWebApi._4.ViewModels.Items;

namespace SampleWebApi._2.Database.Repositories
{
    public interface IItemRepository
    {
        Task<Item> AddAsync(Item item);
        Task<Item> GetByIdAsync(string id);
        Task<List<SimilarItemViewModel>> GetItemListByPriceRangeAsync(decimal minPrice, decimal maxPrice, int count);
        Task<List<Item>> GetOrderedByDateAsync(int top);
        Task<Item> UpdateAsync(Item item);
    }
}