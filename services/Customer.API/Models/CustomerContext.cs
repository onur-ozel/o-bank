using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Customer.API.Models
{
    public partial class CustomerContext : DbContext
    {
        public CustomerContext()
        {
        }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CorporateCustomer> CorporateCustomer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<CustomerPhone> CustomerPhone { get; set; }
        public virtual DbSet<RetailCustomer> RetailCustomer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:localhost,5101;Initial Catalog=Customer;User Id=sa;Password=Strong_Passw0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<CorporateCustomer>(entity =>
            {
                entity.ToTable("corporate_customer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedUser)
                    .HasColumnName("created_user")
                    .HasMaxLength(6);

                entity.Property(e => e.CustomerNumber)
                    .HasColumnName("customer_number")
                    .HasColumnType("numeric(7, 0)")
                    .HasDefaultValueSql("(NEXT VALUE FOR [CustomerNoSequence])");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Industry)
                    .HasColumnName("industry")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Sector)
                    .HasColumnName("sector")
                    .HasMaxLength(100);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10);

                entity.Property(e => e.TaxId)
                    .HasColumnName("tax_id")
                    .HasColumnType("numeric(10, 0)");
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.ToTable("customer_address");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AddressLine)
                    .HasColumnName("address_line")
                    .HasMaxLength(1000);

                entity.Property(e => e.AddressType)
                    .HasColumnName("address_type")
                    .HasMaxLength(10);

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_name")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedUser)
                    .HasColumnName("created_user")
                    .HasMaxLength(6);

                entity.Property(e => e.CustomerNumber)
                    .HasColumnName("customer_number")
                    .HasColumnType("numeric(7, 0)");

                entity.Property(e => e.DistrictName)
                    .HasColumnName("district_name")
                    .HasMaxLength(100);

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postal_code")
                    .HasMaxLength(100);

                entity.Property(e => e.ProvienceName)
                    .HasColumnName("provience_name")
                    .HasMaxLength(100);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CustomerPhone>(entity =>
            {
                entity.ToTable("customer_phone");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedUser)
                    .HasColumnName("created_user")
                    .HasMaxLength(6);

                entity.Property(e => e.CustomerNumber)
                    .HasColumnName("customer_number")
                    .HasColumnType("numeric(7, 0)");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(30);

                entity.Property(e => e.PhoneNumberType)
                    .HasColumnName("phone_number_type")
                    .HasMaxLength(10);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<RetailCustomer>(entity =>
            {
                entity.ToTable("retail_customer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.BirthPlace)
                    .HasColumnName("birth_place")
                    .HasMaxLength(100);

                entity.Property(e => e.CompanyName)
                    .HasColumnName("company_name")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedUser)
                    .HasColumnName("created_user")
                    .HasMaxLength(6);

                entity.Property(e => e.CustomerNumber)
                    .HasColumnName("customer_number")
                    .HasColumnType("numeric(7, 0)")
                    .HasDefaultValueSql("(NEXT VALUE FOR [CustomerNoSequence])");

                entity.Property(e => e.Department)
                    .HasColumnName("department")
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(10);

                entity.Property(e => e.JobTitle)
                    .HasColumnName("job_title")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(100);

                entity.Property(e => e.NationalId)
                    .HasColumnName("national_id")
                    .HasColumnType("numeric(11, 0)");

                entity.Property(e => e.Nationality)
                    .HasColumnName("nationality")
                    .HasMaxLength(2);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10);
            });

            modelBuilder.HasSequence("CustomerNoSequence").StartsAt(100000);
        }
    }
}
