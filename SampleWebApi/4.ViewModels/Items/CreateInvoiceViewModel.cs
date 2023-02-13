using SampleWebApi._1.Entities;
using System.ComponentModel.DataAnnotations;

namespace SampleWebApi._4.ViewModels.Items
{
    public class CreateInvoiceViewModel
    {
        [Required]
        public int AddressId { get; set; }
        public string Description { get; set; }
        [Required]
        public InvoiceType InvoiceType { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; } = 0;
        [Required]
        public DateTime InvoiceDate { get; set; }
        public List<CreateInvoiceLineViewModel> LstLines { get; private set; }
    }

    public class CreateInvoiceLineViewModel
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
