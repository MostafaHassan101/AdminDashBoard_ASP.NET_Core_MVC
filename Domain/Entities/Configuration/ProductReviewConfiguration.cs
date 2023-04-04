using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Configuration
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Review)
                .HasMaxLength(500);

        }
    }
}
