using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Déclaration des DbSet pour chaque modèle existant
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<Delivery>? Deliveries { get; set; }
        public DbSet<IdentityUserRole<string>> UserRoles { get; set; } // Table AspNetUserRoles
        public DbSet<IdentityRole> Roles { get; set; } // Table AspNetRoles
        public DbSet<User> Users { get; set; } // Table AspNetUsers


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de AspNetUsers
            // Lorsque vous utilisez Entity Framework avec ASP.NET Identity,
            // User est mappée à la table AspNetUsers par convention.
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "AspNetUsers");
            });

            // Foreign key relationship (Product => OrderItems)
            modelBuilder.Entity<Product>()
           .HasMany(p => p.OrderItems)
           .WithOne(p => p.Product)
           .HasForeignKey(o => o.ProductId)
           .IsRequired(true);


            // Foreign key relationship (User => Order)
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders) // One-to-many relationship
                      .HasForeignKey(o => o.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Foreign key relationship (User => Delivery)
            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasOne(d => d.User)
                      .WithMany(u => u.Deliveries) // One-to-many relationship
                      .HasForeignKey(d => d.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // La suppression en cascade permet de supprimer automatiquement les enregistrements
            // liés dans les autres tables lorsque vous supprimez un enregistrement parent.

            // One-to-One relationship between User and Cart
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // One - to - One relationship between Order and Delivery
            modelBuilder.Entity<Order>()
                .HasOne(d => d.Delivery)
                .WithOne(d => d.Order)
                .HasForeignKey<Delivery>(d => d.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);  // No action on delete

            // One-to-Many relationship between Order and OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // Virer les datas la table AspNetRoles et essayer le seed
            // Seeding initial des données si nécessaire
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

        }
    }
}
