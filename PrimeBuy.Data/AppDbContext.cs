using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Data
{
    public class AppDbContext : IdentityDbContext<Customer>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Status> Status { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrdersProducts> OrdersProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrdersProducts>().HasKey(x => new {x.ProductId, x.OrderId});
            builder.Entity<Order>()
                .HasMany(x => x.Products)
                .WithMany(x => x.Orders)
                .UsingEntity<OrdersProducts>();
            builder.Entity<Customer>().HasMany(x => x.Orders).WithOne(x => x.Customer);
            builder.Entity<Order>().HasOne(x => x.Status).WithMany(x => x.Orders);
        }
    }
}