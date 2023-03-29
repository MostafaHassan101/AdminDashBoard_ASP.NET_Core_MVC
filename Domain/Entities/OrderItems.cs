using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index (nameof(ProductId), IsUnique = false)]
    public class OrderItems 
    {
        public long Id { get; set; }
        
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
		public virtual Order? Order { get; set; }

    }
}
