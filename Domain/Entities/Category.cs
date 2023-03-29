using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

		public string Name { get; set; }

		public string NameAr { get; set; }

		//public int ParentCategoryId { get; set; }

        public virtual Category? ParentCategory { get; set; }

		public virtual ICollection<Category>? SubCategories { get; set; }

		//public ItemStatus Status { get; protected set; }

		[JsonIgnore]
		public virtual ICollection<Product>? Products { get; set; }

    //    public virtual ICollection<Brand> Brands { get; set; }
    }
}
