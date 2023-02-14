using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi._4.ViewModels.Basket;
using SampleWebApi._4.ViewModels.Items;
using SampleWebApi.Controllers.Base;

namespace SampleWebApi.Controllers
{
    public class InvoiceController : MyBaseController
    {
        public InvoiceController()
        {

        }

        [NonAction]
        [CapSubscribe("Invoice.Build.Basket")]
        public async Task ReceivedBasketInfo(BasketInfoViewModel basket, [FromCap] CapHeader header)
        {

            var userId = header["userId"];
            // process to create invoice

            // 1- check items amount
            // 2- recalculate Price
            // 3- validate all needed validation
            // 4- Create invoice 
        }
    }
}
