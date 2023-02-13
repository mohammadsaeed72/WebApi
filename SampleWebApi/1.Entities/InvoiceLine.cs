using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class InvoiceLine : BaseEntity, IEntity
{
        [ForeignKey(nameof(Item))]
        public string ItemId { get; set; }
        public Item Item { get; set; }

        [ForeignKey(nameof(Invoice))]
        public string InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public InvoiceType InvoiceType { get; set; }

        public int Amount { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal DiscountPerUnit { get; set; } = 0;

        public decimal TotalPrice { get { return Amount * PricePerUnit; } }
        public decimal TotalDiscount { get { return Amount * DiscountPerUnit; } }
        public decimal NetPrice { get { return TotalPrice - TotalDiscount; } }
    }
}
