using SampleWebApi._1.Entities;
using SampleWebApi._4.ViewModels;
using SampleWebApi._4.ViewModels.Basket;

namespace SampleWebApi._3.Services
{
    public interface IBasketService
    {
        Task<ReturnModel<BasketInfoViewModel>> AddItemToBasket(string itemId, int amount, string userID);
        Task<ReturnModel> ClearBasketAsync(string userID);
        Task<ReturnModel<BasketInfoViewModel>> GetBasketAsync(string userID);
        Task<ReturnModel<BasketInfoViewModel>> RemoveItemFromBasket(string itemId, int amount, string userID);
    }
}