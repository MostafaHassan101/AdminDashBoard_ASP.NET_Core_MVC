using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string NameAr { get; set; }

        public string Description { get; set; }

        public string DescriptionAr { get; set; }

        public decimal Price { get; set; }

        public string? Sizes { get; set; }

        public string ImagePath { get; set; }

        public byte Rate { get; set; }

        public byte? Discount { get; set; }

        public int Quantity { get; set; }

        public string ModelNumber { get; set; }

        public virtual Brand? Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual OrderItems? OrderItems { get; set; }

        public virtual ICollection <WishList>? WishLists { get; set; }

        public virtual ICollection<ProductColor> ProductColors { get; set; } 

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<ProductReview>? ProductReview { get; set; }
      
        
        
        
        public bool AddImage(ProductImage image)
        {
            var imageItem = ProductImages.FirstOrDefault(a => a.ImagePath == image.ImagePath);

            if (imageItem == null)
            {
                ProductImages.Add(image);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddColor(ProductColor color)
        {
            var coloritem = ProductColors.FirstOrDefault(a => a.Name == color.Name);

            if (coloritem == null)
            {
                ProductColors.Add(color);
                return true;
            }
            else
            {
                return false;
            }


        }

        public void AddReview(ProductReview review)
        {
            ProductReview.Add(review);
        }
    }
}