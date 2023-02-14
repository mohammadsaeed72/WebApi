using SampleWebApi._1.Entities;

namespace SampleWebApi._4.ViewModels.Basket
{
    public class BasketInfoViewModel
    {
        public BasketInfoViewModel()
        {
            LstBasketLines = new List<BasketLine>();
        }
        public string BasketId { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public int SkuCount { get { return LstBasketLines.Count; } }
        public decimal TotalPrice { get { return LstBasketLines.Sum(a=>a.TotalPrice); } }
        public decimal TotalDiscount { get { return LstBasketLines.Sum(a => a.TotalDiscount); } }
        public List<BasketLine> LstBasketLines { get; private set; }

        public List<BasketLine> AddLine(BasketLine line)
        {
            if (LstBasketLines is null)
                LstBasketLines = new();

            var oldLine = LstBasketLines.FirstOrDefault(a => a.ItemId == line.ItemId);
            if (oldLine is null)
                LstBasketLines.Add(line);
            else
            {
                oldLine.Amount += line.Amount;
                oldLine.DiscountPercentage = line.DiscountPercentage;
                oldLine.PricePerUnit = line.PricePerUnit;
            }
            return LstBasketLines;
        }
        public List<BasketLine> RemoveLine(string itemId,int amount)
        {
            if (LstBasketLines is null)
                LstBasketLines = new();

            var oldLine = LstBasketLines.FirstOrDefault(a => a.ItemId == itemId);
            if (oldLine is not null && oldLine.Amount > amount)
            {
                oldLine.Amount -= amount;
            }
            else if (oldLine is not null && oldLine.Amount <= amount)
                LstBasketLines.Remove(oldLine);

            return LstBasketLines;
        }

        public List<BasketLine> RefreshItemPrice(List<Item> items)
        {
            if (LstBasketLines is null)
                LstBasketLines = new();

            foreach (var item in items)
            {
                var line = LstBasketLines.FirstOrDefault(a => a.ItemId == item.Id);
                if (line is not null )
                {
                    line.PricePerUnit = item.GetPrice(DateTime.Now).Price;
                    line.DiscountPercentage=item.GetDiscountPercent(DateTime.Now);
                }
            }
            
            return LstBasketLines;
        }

    }
}
