

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleWebApi._1.Entities;

namespace SampleWebApi._2.Database.EntitiesConfig
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(a => a.UserName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Name).HasMaxLength(100);
            builder.Property(a => a.LastName).HasMaxLength(100);
        }
    }

    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.DeliveryAddress).IsRequired().HasMaxLength(300);
            builder.Property(a => a.DeliverTo).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Province).IsRequired().HasMaxLength(50);
            builder.Property(a => a.City).IsRequired().HasMaxLength(50);

            builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(10);
            builder.HasIndex(a=>a.PostalCode).IsUnique();

            builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(11);
            

        }
    }
}
