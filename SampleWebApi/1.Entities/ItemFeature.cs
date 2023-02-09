using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class ItemFeature : BaseEntity, IEntity
    {
        [ForeignKey(nameof(Feature))]
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }

        [ForeignKey(nameof(Item))]
        public string ItemId { get; set; }
        public Item Item { get; set; }

        public string Value { get; set; }
    }
}
