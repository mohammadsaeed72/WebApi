using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi._3.Services;
using SampleWebApi._4.ViewModels.Basket;
using SampleWebApi._4.ViewModels.Users;
using SampleWebApi.Controllers.Base;

namespace SampleWebApi.Controllers
{
    public class BasketController : MyBaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost("AddLine")]
        public async Task<IActionResult> AddLineToBasket([FromBody] AddRemoveBasketLineViewModel model)
        {

            var result = await _basketService.AddItemToBasket(model.ItemId,model.Amount,UserId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost("RemoveLine")]
        public async Task<IActionResult> RemoveLineFromBasket([FromBody] AddRemoveBasketLineViewModel model)
        {

            var result = await _basketService.RemoveItemFromBasket(model.ItemId, model.Amount, UserId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("GetBasket")]
        public async Task<IActionResult> GetBasket()
        {

            var result = await _basketService.GetBasketAsync(UserId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("ClearBasket")]
        public async Task<IActionResult> RemoveBasket()
        {

            var result = await _basketService.ClearBasketAsync(UserId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }


        [HttpGet("BuildInvoice")]
        [AllowAnonymous]
        public async Task<IActionResult> BuildInvoiceFromBasket([FromServices] ICapPublisher capBus)
        {

            //var result = await _basketService.GetBasketAsync(UserId);
            //if (result.IsSuccess)
            //{
            //    if(result.Message is not null && result.Message.Length>3)
            //    {
            //        return StatusCode(StatusCodes.Status400BadRequest,result);
            //    }
            //    else
            //    {
            //        var header = new Dictionary<string, string>();
            //        header["userId"] = UserId;

            //        capBus.Publish("Invoice.Build.Basket", result.Data,header);

            //        await _basketService.ClearBasketAsync(UserId);

            //        return Ok(result);
            //    }

            //}
            //return StatusCode(StatusCodes.Status500InternalServerError, result);

            var header = new Dictionary<string, string>();
            header["userId"] = "123123123";

            capBus.Publish("Invoice.Build.Basket", "TestMessage", header);
            return Ok();
        }
    }
}
