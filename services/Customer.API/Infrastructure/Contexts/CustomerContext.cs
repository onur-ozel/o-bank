using Customer.API.Infrastructure.EntityConfigurations;
using Customer.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Infrastructure.Contexts {
    public class CustomerContext : DbContext {

        public CustomerContext (DbContextOptions<CustomerContext> options) : base (options) {
            // this.Database.MigrateAsync();
        }
        public DbSet<CorporateCustomerItem> CorporateCustomerItems { get; set; }
        public DbSet<RetailCustomerItem> RetailCustomerItems { get; set; }

        protected override void OnModelCreating (ModelBuilder builder) {
            builder.HasSequence<long> ("CustomerNumbers")
                .StartsAt (100000)
                .IncrementsBy (1);

            builder.ApplyConfiguration (new CorporateCustomerItemEntityTypeConfiguration ());
            builder.ApplyConfiguration (new RetailCustomerItemEntityTypeConfiguration ());
        }
    }
}