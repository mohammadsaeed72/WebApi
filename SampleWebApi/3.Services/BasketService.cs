using Microsoft.Extensions.Caching.Distributed;
using SampleWebApi._0.Helpers;
using SampleWebApi._1.Entities;
using SampleWebApi._2.Database.Repositories;
using SampleWebApi._4.ViewModels;
using SampleWebApi._4.ViewModels.Basket;

namespace SampleWebApi._3.Services
{
    public class BasketService : IBasketService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IDistributedCache _cache;

        public BasketService(IItemRepository itemRepository, IDistributedCache cache)
        {
            _itemRepository = itemRepository;
            _cache = cache;
        }

        public async Task<ReturnModel<BasketInfoViewModel>> GetBasketAsync(string userID)
        {
            var returnVal = new ReturnModel<BasketInfoViewModel>();
            if (userID is null || string.IsNullOrEmpty(userID))
            {
                returnVal.IsSuccess = false;
                returnVal.Message = "کاربر را ارسال نمایید";
                return returnVal;
            }

            var basketFromCache = _cache.GetRecord<BasketInfoViewModel>(DistCacheKey.BasketKey + userID);
            if (basketFromCache is null)
            {
                returnVal.IsSuccess = false;
                returnVal.Message = "سبدی برای این کاربر یافت نشد";
                return returnVal;
            }

            List<Item> lstItems = new();

            foreach (var (itemId, amount, name) in basketFromCache.LstBasketLines.Select(a => (a.ItemId, a.Amount, a.ItemName)).ToList())
            {
                var item = await _cache.GetRecordAsync<Item>(DistCacheKey.ItemShort + itemId);
                if (item is null)
                    item = await _itemRepository.GetByIdAsync(itemId);
                if (item is null)
                {
                    returnVal.IsSuccess = false;
                    returnVal.Message = "کد کالا با شناسه ارسالی یافت نشد";
                    return returnVal;
                }
                if (item.GetItemAmount(DateTime.Now) < amount)
                    returnVal.Message += $"کالای {name} به مقدار کافی در انبار موجود نمی باشد";

                await _cache.SetRecordAsync(DistCacheKey.ItemShort + itemId, item, TimeSpan.FromSeconds(30));
                lstItems.Add(item);
            }

            basketFromCache.RefreshItemPrice(lstItems);

            await _cache.SetRecordAsync<BasketInfoViewModel>(DistCacheKey.BasketKey + userID, basketFromCache, TimeSpan.FromDays(7));

            returnVal.Data = basketFromCache;
            returnVal.IsSuccess = true;
            return returnVal;

        }


        public async Task<ReturnModel> ClearBasketAsync(string userID)
        {
            var returnVal = new ReturnModel();
            if (userID is null || string.IsNullOrEmpty(userID))
            {
                returnVal.IsSuccess = false;
                returnVal.Message = "کاربر را ارسال نمایید";
                return returnVal;
            }
            await _cache.DeleteRecordAsync(DistCacheKey.BasketKey + userID);

            return returnVal;

        }

        public async Task<ReturnModel<BasketInfoViewModel>> AddItemToBasket(string itemId, int amount, string userID)
        {
            var returnVal = new ReturnModel<BasketInfoViewModel>();
            if (userID is null || string.IsNullOrEmpty(userID))
            {
                returnVal.IsSuccess = false;
                returnVal.Message = "کاربر را ارسال نمایید";
                return returnVal;
            }
            var item = await _cache.GetRecordAsync<Item>(DistCacheKey.ItemShort + itemId);
            if (item is null)
                item = await _itemRepository.GetByIdAsync(itemId);
            if (item is null)
            {
                returnVal.IsSuccess = false;
                returnVal.Message = "کد کالا با شناسه ارسالی یافت نشد";
                return returnVal;
            }
            await _cache.SetRecordAsync(DistCacheKey.ItemShort + itemId, item, TimeSpan.FromSeconds(30));

            if (item.GetItemAmount(DateTime.Now) < amount)
            {
                returnVal.IsSuccess = false;
                returnVal.Message = "موجودی کالا، کمتر از مقدار درخواستی شما می باشد";
                return returnVal;
            }

            var basketFromCache = _cache.GetRecord<BasketInfoViewModel>(DistCacheKey.BasketKey + userID);
            if (basketFromCache is null)
            {
                basketFromCache = new BasketInfoViewModel
                {
                    BasketId = Guid.NewGuid().ToString(),
                    UserId = userID
                };
            }
            var basketLine = new BasketLine()
            {
                Amount = amount,
                DiscountPercentage = item.GetDiscountPercent(DateTime.Now),
                ItemCode = item.Code,
                ItemId = item.Id,
                ItemName = item.Name,
                PricePerUnit = item.GetPrice(DateTime.Now).Price
            };
            basketFromCache.AddLine(basketLine);

            await _cache.SetRecordAsync<BasketInfoViewModel>(DistCacheKey.BasketKey + userID, basketFromCache, TimeSpan.FromDays(7));

            returnVal.Data = basketFromCache;
            returnVal.IsSuccess = true;
            return returnVal;

        }

        public async Task<ReturnModel<BasketInfoViewModel>> RemoveItemFromBasket(string itemId, int amount, string userID)
        {
            var returnVal = new ReturnModel<BasketInfoViewModel>();
            if (userID is null || string.IsNullOrEmpty(userID))
            {
                returnVal.IsSuccess = false;
                returnVal.Message = "کاربر را ارسال نمایید";
                return returnVal;
            }


            var basketFromCache = await _cache.GetRecordAsync<BasketInfoViewModel>(DistCacheKey.BasketKey + userID);
            if (basketFromCache is null)
            {
                returnVal.IsSuccess = false;
                returnVal.Message = "سبدی برای این کاربر یافت نشد";
                return returnVal;
            }

            var basketLines = basketFromCache.RemoveLine(itemId, amount);

            if (basketLines.Count == 0)
                await _cache.DeleteRecordAsync(DistCacheKey.BasketKey + userID);
            else
                await _cache.SetRecordAsync<BasketInfoViewModel>(DistCacheKey.BasketKey + userID, basketFromCache, TimeSpan.FromDays(7));

            returnVal.Data = basketFromCache;
            returnVal.IsSuccess = true;
            return returnVal;

        }


    }
}
