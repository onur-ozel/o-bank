using Customer.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.API.Infrastructure.EntityConfigurations {
    class CorporateCustomerItemEntityTypeConfiguration
        : IEntityTypeConfiguration<CorporateCustomerItem> {
            public void Configure (EntityTypeBuilder<CorporateCustomerItem> builder) {
                builder.ToTable ("CorporateCustomer");

                builder.HasKey (ci => ci.Id);
                builder.Property (ci => ci.Id)
                    .IsRequired ();

                builder.Property (ci => ci.CustomerNo)
                    .ValueGeneratedOnAdd ()
                    .HasDefaultValueSql ("NEXT VALUE FOR CustomerNoSequence");

                builder.Property (ci => ci.Name)
                    .HasMaxLength (250);

                builder.Property (ci => ci.Industry)
                    .HasMaxLength (250);

                builder.Property (ci => ci.Sector)
                    .HasMaxLength (250);

                builder.Property (ci => ci.TaxId)
                    .HasMaxLength (10);

                builder.Property (ci => ci.Email)
                    .HasMaxLength (100);

                builder.Property (ci => ci.WebSite)
                    .HasMaxLength (100);

                builder.Property (ci => ci.CreatedDate);

                builder.Property (ci => ci.CreatedUser)
                    .HasMaxLength (15);
            }
        }
}