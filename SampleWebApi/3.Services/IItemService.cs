using SampleWebApi._4.ViewModels.Items;

namespace SampleWebApi._3.Services
{
    public interface IItemService
    {
        Task<List<GetItemViewModel>> GetItemsAsync(int page = 1, int pageSize = 10);
        Task<List<SimilarItemViewModel>> GetSimilarItemByPriceAsync(string itemId, int count = 3, decimal deviation = 0.01M);
        Task SoftDeleteAsync(string itemId);
    }
}