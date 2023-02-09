using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class ItemPrice : BaseEntity, IEntity
    {
        public decimal Price { get; set; }

        [ForeignKey(nameof(Item))]
        public string ItemId { get; set; }
        public Item Item { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; } = DateTime.MaxValue;
    }
}
