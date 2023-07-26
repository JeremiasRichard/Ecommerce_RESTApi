using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RemitoApi.Entities;
using System.Security.Cryptography.X509Certificates;

namespace RemitoApi.DataBase
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductType>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProductOrigin>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<CategoryType>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Category>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.CategoryType)
                .WithMany()
                .HasForeignKey(c => c.CategoryTypeId);
            
        }

        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductOrigin> ProductOrigins { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
