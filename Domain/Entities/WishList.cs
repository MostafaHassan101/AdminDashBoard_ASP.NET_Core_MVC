using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WishList
    {
        public long Id { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }


    }
}
