

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleWebApi._1.Entities;

namespace SampleWebApi._2.Database.EntitiesConfig
{
    public partial class ItemConfig
    {
        public class ItemTransactionConfig : IEntityTypeConfiguration<ItemTransaction>
        {
            public void Configure(EntityTypeBuilder<ItemTransaction> builder)
            {
                builder.Navigation(a => a.InvoiceLine).AutoInclude(true);
                builder.HasOne(a => a.Item).WithOne().OnDelete(DeleteBehavior.NoAction);
            }
        }
    }
}
