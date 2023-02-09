using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class Item:BaseEntity<string>,IEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int IsActive { get; set; }

        [ForeignKey(nameof(Group))]
        public int? ItemGroupId { get; set; }
        public ItemGroup Group { get; set; }
        public List<ItemPrice> LstItemPrice { get; set; }
        public List<ItemDiscount> LstDiscounts { get; set; }
        public List<ItemFeature> LstFeatures { get; set; }


        public decimal GetDiscountPercent(DateTime dateTime)
        {
            var discount= GetDiscount(dateTime);
            if(discount is null)
                return 0;
            return discount.Discount;
        }
        public decimal GetDiscountToman(DateTime dateTime)
        {
            var discount = GetDiscount(dateTime);
            var Price = GetPrice(dateTime);
            if (discount is null || Price is null)
                return 0;
            return discount.Discount*Price.Price;
        }

        private ItemDiscount GetDiscount(DateTime dateTime)
        {
            ItemDiscount discount = null;
            if (LstDiscounts is not null)
            {
                 discount = LstDiscounts.FirstOrDefault(a => a.FromDate <= dateTime && a.ToDate >= dateTime);
            }
            return discount;
        }
        public ItemPrice GetPrice(DateTime dateTime)
        {
            ItemPrice price = null;
            if (LstItemPrice is not null)
            {
                price = LstItemPrice.FirstOrDefault(a => a.FromDate <= dateTime && a.ToDate >= dateTime);
            }
            return price;
        }
    }
}
