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

                builder.Property (ci => ci.CustomerNo)
                    .ValueGeneratedOnAdd ()
                    .HasDefaultValueSql ("NEXT VALUE FOR CustomerNoSequence");

                builder.Property (ci => ci.NationalId)
                    .HasMaxLength (11);

                builder.Property (ci => ci.FirstName)
                    .HasMaxLength (100);

                builder.Property (ci => ci.LastName)
                    .HasMaxLength (100);

                builder.Property (ci => ci.Nationality)
                    .HasMaxLength (2);

                builder.Property (ci => ci.BirthDate);

                builder.Property (ci => ci.Email)
                    .HasMaxLength (100);

                builder.Property (ci => ci.Gender)
                    .HasMaxLength (1);

                builder.Property (ci => ci.CreatedDate);

                builder.Property (ci => ci.CreatedUser)
                    .HasMaxLength (15);
            }
        }
}