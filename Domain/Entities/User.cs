using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class User : IdentityUser<long>
	{
        public string FirstName { get; protected set; }

		public string LastName { get; protected set; }

		public string? Address { get; protected set; }

		public string? City { get; protected set; }

		public string? Phone { get; protected set; }

		public virtual ICollection<Order>? Orders { get; protected set; }

		public virtual ICollection<ProductReview>? Reviews { get; protected set; }

		public virtual WishList? WishList { get; protected set; }

	}
}
