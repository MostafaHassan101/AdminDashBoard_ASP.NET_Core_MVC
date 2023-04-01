using Domain.Entities;

namespace AdminDashboard.Models
{
    public class OrderItemsModel
    {
        public int  Quantity { get; set; }
        public Product Product { get; set; }
    }
}
