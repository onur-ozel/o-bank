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

                builder.Property (ci => ci.No)
                    .ValueGeneratedOnAdd ()
                    .HasDefaultValueSql ("NEXT VALUE FOR CustomerNumbers");

                builder.Property (ci => ci.Name)
                    .IsRequired (true)
                    .HasMaxLength (150);

                builder.Property (ci => ci.TaxId)
                    .IsRequired (true);
            }
        }
}