using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi._3.Services;
using SampleWebApi._4.ViewModels.Items;
using SampleWebApi.Controllers.Base;

namespace SampleWebApi.Controllers
{
    public class ItemController : MyBaseController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet("Items")]
        [AllowAnonymous]
        public async  Task<IActionResult> GetItemsPaging(int page, int pageSize)
        {
            var result =await _itemService.GetItemsAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("SimilarItem")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSimilarItem(string itemId,int count,decimal deviation=0.1M)
        {
            var result = await _itemService.GetSimilarItemByPriceAsync(itemId,count,deviation);
            return Ok(result);
        }

        [HttpDelete("SoftDeleteItem")]
        public async Task<IActionResult> SoftDelete(string itemId)
        {
             await _itemService.SoftDeleteAsync(itemId);
            return Ok();
        }
    }
}
