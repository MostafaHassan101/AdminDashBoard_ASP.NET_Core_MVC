using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Context
{
	public static class RelationsMapping
    {
        public static void MapRelations(this ModelBuilder modelBuilder)
        {
            /// Product Relations
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Category);
            
    //        modelBuilder.Entity<Product>()
				//.HasOne(p => p.Brand);
            //poductImages
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(i => i.Product)
                .HasForeignKey( p => p.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductReview)
                .WithOne(r => r.Product)
                .HasForeignKey(r=>r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

         
            /// Category Relations
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.Cascade);

              
            //subCategory
            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .OnDelete(DeleteBehavior.Cascade);

            /// Brand Relations
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Products)
                .WithOne(p => p.Brand)
				.OnDelete(DeleteBehavior.Cascade);


 

            modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Product>()
            .HasMany(p => p.WishLists)
            .WithMany(w => w.Products);


            modelBuilder.Entity<User>()
           .HasMany(u => u.Orders)
           .WithOne(r => r.User)
           .HasForeignKey(o=>o.UserId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
				.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                        .HasOne(b => b.WishList)
                        .WithOne(i => i.User)
			            .HasForeignKey<WishList>(b => b.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

          
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
