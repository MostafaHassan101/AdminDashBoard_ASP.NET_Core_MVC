using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }

		public string Address { set; get; }


		public decimal TotalPrice { get; set; }

        public int? Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }

        public string UserPhone { get;  set; }

        //[JsonIgnore]
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }

    }
}
