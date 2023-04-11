using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Configuration
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {

		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Id)
				.ValueGeneratedOnAdd();

			builder.Property(s => s.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(s => s.NameAr)
				.IsRequired()
				.HasMaxLength(100);


			builder.Property(s => s.Description)
				.IsRequired()
				.HasMaxLength(1000);

			builder.Property(s => s.DescriptionAr)
				.IsRequired()
				.HasMaxLength(1000);

			builder.Property(s => s.Price)
				.IsRequired()
				.HasColumnType("decimal(18,2)");

            builder.Property(s => s.Sizes)
                .HasMaxLength(50);

            builder.Property(s => s.ImagePath)
                .IsRequired();/////////////////////////////

            builder.Property(p => p.Rate)
				.HasDefaultValue(0);

            builder.Property(s => s.Quantity)
                .IsRequired();/////////////////////////////

            builder.Property(s => s.ModelNumber)
                .IsRequired()
                .HasMaxLength(100);/////////////////////////////

            //builder.HasMany(s => s.Images)
            //	.WithOne(s => s.product)
            //	.OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(s => s.Orders)
            //	.WithOne(s => s.Product)
            //	.OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(s => s.Category).WithMany(s => s.Products);
            //.HasForeignKey(x => x.ProductCategoryId);

            //builder.HasMany(x => x.ProductColors)
            //	.WithOne(y => y.Product)
            //	.HasForeignKey(y => y.ProductId);

            //public void Configure(EntityTypeBuilder<Product> builder)
            //{
            //    builder.ToTable("Products");
            //    builder.HasKey(p => p.Id);
            //    builder.Property(p => p.Id).ValueGeneratedOnAdd();
            //    builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            //    builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            //    builder.Property(p => p.Price).IsRequired();
            //    builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            //    builder.Property(p => p.Stock).IsRequired();
            //    builder.Property(p => p.Discount).IsRequired().HasDefaultValue(0);
            //    builder.Property(p => p.Rate).IsRequired().HasDefaultValue(0);
            //    //builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
            //    //builder.HasOne(p => p.Brand).WithMany().HasForeignKey(p => p.BrandId);
            //    //builder.HasMany(p => p.Images).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
            //    //builder.HasMany(p => p.Reviews).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);
            //    //builder.HasMany(p => p.Specifications).WithOne(p => p.Product).HasForeignKey(p => p.ProductId);

            //}
        }
    }
}
