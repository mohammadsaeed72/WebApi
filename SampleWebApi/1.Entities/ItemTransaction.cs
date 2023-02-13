using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class ItemTransaction : BaseEntity, IEntity
    {
        [ForeignKey(nameof(Item))]
        public string ItemId { get; set; }
        public Item Item { get; set; }

        [ForeignKey(nameof(InvoiceLine))]
        public int InvoiceLineId { get; set; }
        public InvoiceLine InvoiceLine { get; set; }

    }
}
