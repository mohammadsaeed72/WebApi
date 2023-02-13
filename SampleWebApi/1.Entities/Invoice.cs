using SampleWebApi._1.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebApi._1.Entities
{
    public class Invoice : BaseEntity<string>, IEntity
    {
        [ForeignKey(nameof(User))]
        public int AppUserId { get; set; }
        public AppUser User { get; set; }

        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DeliverDate { get; set; }
        public string Description { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public decimal TotalPrice { get; set; }
        public int SkuCount { get { return LstLines.Count(); } }
        public decimal TotalDiscount { get; set; } = 0;

        public List<InvoiceLine> LstLines { get; private set; }

        public List<InvoiceLine> AddLine(InvoiceLine line)
        {
            if (LstLines is null)
                LstLines = new();

            var l = LstLines.FirstOrDefault(a => a.ItemId == line.ItemId);
            if (l is null)
                LstLines.Add(line);

            return LstLines;

        }

        public List<InvoiceLine> AddLines(List<InvoiceLine> lines)
        {
            if (LstLines is null)
                LstLines = new();

            foreach (var line in lines)
            {
                if (LstLines.Any(a => a.ItemId == line.ItemId) == false)
                    LstLines.Add(line);
            }
            return LstLines;

        }

        public List<InvoiceLine> RemoveLine(InvoiceLine line)
        {
            if (LstLines is null)
                LstLines = new();

            var l = LstLines.FirstOrDefault(a => a.ItemId == line.ItemId);
            if (l is not null)
                LstLines.Remove(l);
            return LstLines;
        }
        public List<InvoiceLine> RemoveLines(List<InvoiceLine> lines)
        {
            if (LstLines is null)
                LstLines = new();

            foreach (var line in lines)
            {
                var l = LstLines.FirstOrDefault(a => a.ItemId == line.ItemId);
                if (l is not null)
                    LstLines.Remove(l);
            }
            return LstLines;
        }

        public List<InvoiceLine> EditAmount(string itemId, int addedAmount)
        {
            if (LstLines is null)
                LstLines = new();

            var l = LstLines.FirstOrDefault(a => a.ItemId == itemId);
            if (l is null)
                l.Amount += addedAmount;

            return LstLines;

        }

    }
}
