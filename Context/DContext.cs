using Domain.Entities;
using Domain.Entities.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public class DContext : IdentityDbContext<User,IdentityRole<long>,long>
    {
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItems> OrderDetails { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<WishList> WishList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BrandConfiguration().Configure(modelBuilder.Entity<Brand>());
            new CategoryConfigration().Configure(modelBuilder.Entity<Category>());
            new OrderConfigration().Configure(modelBuilder.Entity<Order>());
            new OrderItemsConfigration().Configure(modelBuilder.Entity<OrderItems>());
            new ProductConfigration().Configure(modelBuilder.Entity<Product>());
            new ProductImagesConfiguration().Configure(modelBuilder.Entity<ProductImage>());
            new WishListConfiguration().Configure(modelBuilder.Entity<WishList>());
            new ProductReviewConfiguration().Configure(modelBuilder.Entity<ProductReview>());
            new ProductColorConfiguration().Configure(modelBuilder.Entity<ProductColor>());
            base.OnModelCreating(modelBuilder);
        }
        //      protected override void OnConfiguring(DbContextOptionsBuilder options)
        //      {
        //          options.UseSqlServer(
        //          "Data Source=DESKTOP-K0QTQH2\\SQLEXPRESS01;Initial Catalog = Noon;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False");
        //}
        public DContext(DbContextOptions<DContext> options) : base(options)
        {

        }

    }
}