namespace Domain.Entities
{
    public class ProductColor
	{
		public long Id { get; protected set; }

		//public long ProductId { get; protected set; }

		public string Name { get;  set; }

		public string HexValue { get;  set; }

		public virtual ICollection<Product> Products { get; protected set; }
		//public virtual Product Product { get; protected set; }
	}
}
