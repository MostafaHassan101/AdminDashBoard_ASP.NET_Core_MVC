using Domain.Entities;

namespace AdminDashboard.Models
{
    public class OrderModel
    {
        public string Address { set; get; }
        public decimal TotalPrice { get; set; }
        public int? Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }

        public string UserPhone { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
