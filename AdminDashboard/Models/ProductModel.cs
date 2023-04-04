using Domain.Entities;

namespace AdminDashboard.Models
{
    public class ProductModel
    {
        public string Name { get; set; }

        public string NameAr { get; set; }

        public string Description { get; set; }

        public string DescriptionAr { get; set; }

        public decimal Price { get; set; }

        public string? Sizes { get; set; }

        public IFormFile ImagePath { get; set; }
        public byte? Discount { get; set; }

        public int Quantity { get; set; }

        public string ModelNumber { get; set; }

        public long CategoryId { get; set; }

        public long? BrandId { get; set; }
  
        public List<IFormFile>Images { get; set; }
        public virtual ICollection<long> ProductColorsidsIds { get; set; }

    }
}
