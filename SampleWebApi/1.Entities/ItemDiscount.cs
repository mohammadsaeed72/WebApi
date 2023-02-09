using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class ItemDiscount : BaseEntity, IEntity
    {
        public decimal Discount { get; set;}
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        [ForeignKey(nameof(Item))]
        public string ItemId { get; set; }
        public Item Item { get; set; }
    }
}
