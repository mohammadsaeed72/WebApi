using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi._4.ViewModels.Basket;
using SampleWebApi._4.ViewModels.Items;
using SampleWebApi.Controllers.Base;

namespace SampleWebApi.Controllers
{
    public class InvoiceController : MyBaseController
    {
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(ILogger<InvoiceController> logger)
        {
            _logger = logger;
        }

        [NonAction]
        [CapSubscribe("Invoice.Build.Basket")]
        public async Task ReceivedBasketInfo(string basket, [FromCap] CapHeader header)
        {

            var userId = header["userId"];

            _logger.LogError($"{userId} in Queue");
            Console.WriteLine("Invoice.Build.Basket");
            // process to create invoice

            // 1- check items amount
            // 2- recalculate Price
            // 3- validate all needed validation
            // 4- Create invoice 
        }
    }
}
