using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._4.ViewModels.Basket
{
    public class AddRemoveBasketLineViewModel
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
