using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
        }
    }
}
