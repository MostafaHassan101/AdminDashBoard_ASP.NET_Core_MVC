using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Context
{
    public static class RelationsMapping
    {
        public static void MapRelations(this ModelBuilder modelBuilder)
        {
            /// Product Relations
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category);
            
            modelBuilder.Entity<Product>()
				.HasOne(p => p.Brand);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(i => i.Product)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductReview)
                .WithOne(r => r.Product)
                .OnDelete(DeleteBehavior.Cascade);

            /// Category Relations
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.Cascade);
              

            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .OnDelete(DeleteBehavior.Cascade);

            /// Brand Relations
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Products)
                .WithOne(p => p.Brand)
				.OnDelete(DeleteBehavior.Cascade);

            /// Order Relations
            modelBuilder.Entity<Order>()
				.HasOne(o => o.User);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
				.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishList>()
				.HasMany(w => w.Products)
				.WithOne(p => p.WishList)
                .OnDelete(DeleteBehavior.NoAction);

			/// ProductReview Relations
             modelBuilder.Entity<Product>()
                .HasMany(r => r.ProductReview)
                .WithOne(p => p.Product);

            /// User Relations
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
				.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
				.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(b => b.WishList)
                .WithOne(i => i.User)
			    .HasForeignKey<WishList>(b => b.UserId);

          
            modelBuilder.Entity<Product>()
			    .HasOne(b => b.OrderItems)
			    .WithOne(i => i.Product)
			    .HasForeignKey<OrderItems>(b => b.ProductId);

            modelBuilder.Entity<OrderItems>()
                .HasIndex(o => o.ProductId)
                .IsUnique(false);

        }
    }
}
