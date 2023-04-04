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
        }
    }
}
