

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleWebApi._1.Entities;

namespace SampleWebApi._2.Database.EntitiesConfig
{
    public partial class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(100);
            builder.Property(a => a.ShortDescription).HasMaxLength(250);
            builder.Property(a => a.Description).HasMaxLength(1500);
            builder.Property(a => a.Code).HasMaxLength(20);
            builder.HasIndex(a=>a.Code).IsUnique();

            builder.HasQueryFilter(a => a.IsActive == true);
        }
    }

    public partial class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasOne(a => a.User).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
