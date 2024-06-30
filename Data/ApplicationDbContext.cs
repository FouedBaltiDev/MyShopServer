using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Déclaration des DbSet pour chaque modèle
        public DbSet<User>? Users { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<Delivery>? Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "BaltiFoued", Password = "123456", Email = "fbalti@gmail.com", Role = "Admin", Address = "85 cité ferdaouis", PhoneNumber = "29201085" },
                new User { Id = 2, UserName = "BaltiWiem", Password = "123456", Email = "wbalti@gmail.com", Role = "Customer", Address = "13 cité santé agba", PhoneNumber = "51377057" }
            );

            // Seeding Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "T-Shirt", Description = "Description1", Price = 10.99m, Stock = 100 },
                new Product { Id = 2, Name = "Bucket Hat", Description = "Description2", Price = 20.99m, Stock = 50 }
            );

            // Seeding Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderDate = DateTime.Now, TotalAmount = 21.98m, Status = "Pending", UserId = 1 },
                new Order { Id = 2, OrderDate = DateTime.Now, TotalAmount = 20.99m, Status = "Completed", UserId = 2 }
            );

            // Seeding OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, ProductId = 1, Quantity = 2, UnitPrice = 10.99m, OrderId = 1 },
                new OrderItem { Id = 2, ProductId = 2, Quantity = 1, UnitPrice = 20.99m, OrderId = 2 }
            );

            // Seeding Carts
            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = 1, UserId = 1 },
                new Cart { Id = 2, UserId = 2 }
            );

            // Seeding Deliveries
            modelBuilder.Entity<Delivery>().HasData(
                new Delivery { Id = 1, OrderId = 1, DeliveryDate = DateTime.Now.AddDays(2), Address = "85 cité ferdaouis", Status = "Shipped" },
                new Delivery { Id = 2, OrderId = 2, DeliveryDate = DateTime.Now.AddDays(1), Address = "13 cité santé agba", Status = "Delivered" }
            );
        }
    }
}
