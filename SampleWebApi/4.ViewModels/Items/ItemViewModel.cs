using SampleWebApi._1.Entities;
using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._4.ViewModels.Items
{
    public class GetItemViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int? ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal DiscountValue { get; set; } = 0;
        public virtual List<ItemFeatureViewModel> LstFeatures { get; set; }
        public int Inventory { get; set; }
    }
}
