using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._4.ViewModels.Items
{
    public class AddDiscountViewModel
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public decimal DiscountPercent { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
    }
}
