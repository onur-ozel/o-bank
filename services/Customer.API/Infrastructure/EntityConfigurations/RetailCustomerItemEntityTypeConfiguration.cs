using Customer.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.API.Infrastructure.EntityConfigurations {
    class RetailCustomerItemEntityTypeConfiguration
        : IEntityTypeConfiguration<RetailCustomerItem> {
            public void Configure (EntityTypeBuilder<RetailCustomerItem> builder) {
                builder.ToTable ("RetailCustomer");

                builder.HasKey (ci => ci.Id);
                builder.Property (ci => ci.Id)
                    .IsRequired ();

                builder.Property (ci => ci.No)
                    .ValueGeneratedOnAdd ()
                    .HasDefaultValueSql ("NEXT VALUE FOR CustomerNumbers");

                builder.Property (ci => ci.FirstName)
                    .IsRequired (true)
                    .HasMaxLength (150);

                builder.Property (ci => ci.LastName)
                    .IsRequired (true)
                    .HasMaxLength (150);

                builder.Property (ci => ci.NationalId)
                    .IsRequired (true);
            }
        }
}