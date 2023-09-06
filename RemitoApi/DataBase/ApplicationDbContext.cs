using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RemitoApi.Entities;
using RemitoApi.Models;
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

            modelBuilder.Entity<ProductType>(productType =>
            {

                productType.HasKey(p => p.Id);

                productType.HasMany(p => p.Products)
                .WithOne(c => c.ProductType)
                .HasForeignKey(p => p.Id);

            });


            modelBuilder.Entity<ProductOrigin>(productOrigin =>
            {

                productOrigin.HasKey(p => p.Id);

                productOrigin.HasMany(p => p.Products)
                .WithOne(po => po.ProductOrigin)
                .HasForeignKey(po => po.ProductOriginId);
            });

            modelBuilder.Entity<Product>(product =>
            {

                product.HasKey(p => p.Id);

                product.HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId);

                product.HasOne(p => p.ProductOrigin)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ProductOriginId);

                product.HasOne(p => p.ProductType)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ProductTypeId);

                product.HasMany(c => c.Items)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.Id);

                product.Property(p => p.ProductName).HasMaxLength(30).IsRequired();
                product.Property(p => p.Quantity).IsRequired();
                product.Property(p => p.ProductOriginId).IsRequired();
                product.Property(p => p.ProductTypeId).IsRequired();
                product.Property(p => p.CategoryId).IsRequired();
                product.Property(p => p.Price).IsRequired();

            });


            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(p => p.Id);

                category.HasMany(p => p.Products)
                 .WithOne(p => p.Category)
                 .HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Items>(items =>
            {

                items.HasKey(i => i.Id);

                items.HasOne(p => p.Product)
                .WithMany(c => c.Items)
                .HasForeignKey(p => p.ProductId);

            });


            modelBuilder.Entity<DeliveryNote>(deliveryNote =>
            {
                deliveryNote.HasKey(dn => dn.Id);
            });
        }

        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductOrigin> ProductOrigins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
    }
}
