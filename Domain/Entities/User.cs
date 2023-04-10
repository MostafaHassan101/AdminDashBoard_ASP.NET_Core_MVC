﻿using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class User : IdentityUser<long>
	{
        public string FirstName { get; set; }

		public string LastName { get; set; }

		public string? Address { get;  set; }

		public string? City { get; set; }

		public string? Phone { get;  set; }

		public virtual ICollection<Order>? Orders { get; set; }

		public virtual ICollection<ProductReview>? Reviews { get; set; }

		public virtual WishList? WishList { get; set; }

	}
}
