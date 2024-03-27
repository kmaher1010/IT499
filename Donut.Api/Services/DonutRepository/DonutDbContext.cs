using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

namespace Library.WebApi.Services.DonutRepository {
    public class DonutDbContext : DbContext {
        public DonutDbContext(DbContextOptions<DonutDbContext> options) : base(options) {
        }


        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder mb) {
            mb.Entity<Customer>().HasKey(c => c.CustomerId);
            mb.Entity<Product>().HasKey(p => p.ProductId);
            mb.Entity<Orders>().HasKey(o => o.OrderId);
            mb.Entity<OrderItems>().HasKey(oi => oi.OrderItemId);

            mb.Entity<Orders>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Orders)
                .HasForeignKey(oi => oi.OrderId);
        }
    }

}
