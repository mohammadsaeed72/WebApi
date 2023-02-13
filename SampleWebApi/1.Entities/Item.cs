
using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class Item : BaseEntity<string>, IEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey(nameof(Group))]
        public int? ItemGroupId { get; set; }
        public ItemGroup Group { get; set; }
        public virtual List<ItemPrice> LstItemPrice { get; set; }
        public virtual List<ItemDiscount> LstDiscounts { get; set; }
        public virtual List<ItemFeature> LstFeatures { get; set; }
        public virtual List<ItemTransaction> LstItemTransactions { get; set; }


        public decimal GetDiscountPercent(DateTime dateTime)
        {
            var discount = GetDiscount(dateTime);
            if (discount is null)
                return 0;
            return discount.Discount;
        }
        public decimal GetDiscountToman(DateTime dateTime)
        {
            var discount = GetDiscount(dateTime);
            var Price = GetPrice(dateTime);
            if (discount is null || Price is null)
                return 0;
            return discount.Discount * Price.Price;
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

        public int GetItemAmount(DateTime dateTime)
        {
            // ToDo adding snapshot to Speed it up
            if (LstItemTransactions is null || LstItemTransactions.Count <= 0)
            {
                return 0;
            }
            var input = LstItemTransactions.Where(a => a.InsertedDate <= dateTime && (a.InvoiceLine.InvoiceType == InvoiceType.ReturnSale || a.InvoiceLine.InvoiceType == InvoiceType.Buy)).
                Sum(a => a.InvoiceLine.Amount);
            var output = LstItemTransactions.Where(a => a.InsertedDate <= dateTime && (a.InvoiceLine.InvoiceType == InvoiceType.ReturnBuy || a.InvoiceLine.InvoiceType == InvoiceType.Sale)).
                Sum(a => a.InvoiceLine.Amount);

            return input - output;
        }
    }
}
