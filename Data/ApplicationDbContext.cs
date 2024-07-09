using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserRole> // Utilisation de IdentityDbContext<UserRole> pour intégrer UserRole dans Identity
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Déclaration des DbSet pour chaque modèle existant
        public new DbSet<User>? Users { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<Delivery>? Deliveries { get; set; }

        // Ajout de DbSet pour UserRole
        public new DbSet<UserRole>? UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de UserRole
            // à virer
            modelBuilder.Entity<UserRole>().ToTable("UserRoles"); // Nom de la table si différent de UserRole par défaut
            modelBuilder.Entity<UserRole>().HasKey(e => e.Id); // Clé primaire
                                                               // Autres configurations selon les besoins

            // Configuration de AspNetUsers
            // Lorsque vous utilisez Entity Framework avec ASP.NET Identity,
            // IdentityUser est mappée à la table AspNetUsers par convention.
            // Renommer la table UserRole to AspNetUsers
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "AspNetUsers");
            });

            // Seeding initial des données si nécessaire
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            // Seeding des autres entités comme vous l'avez déjà configuré
            // Exemple :
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
