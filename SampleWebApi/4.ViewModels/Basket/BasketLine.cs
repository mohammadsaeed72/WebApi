namespace SampleWebApi._4.ViewModels.Basket
{
    public class BasketLine
    {
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int Amount { get; set; } = 0;
        public decimal PricePerUnit { get; set; } = 0;
        public decimal DiscountPercentage { get; set; } = 0;
        public decimal TotalPrice { get { return Amount * PricePerUnit; } }
        public decimal TotalDiscount { get { return Amount * DiscountPercentage; } }
    }
}
