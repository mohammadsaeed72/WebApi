using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._4.ViewModels.Items
{
    public class AddItemPriceViewModel
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
    }

    
}
