using System.Drawing;

namespace Domain.Entities
{
    public class ProductColor
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string HexValue { get;  set; }

		public virtual ICollection<Product> Products { get; protected set; }
		
	}
}
