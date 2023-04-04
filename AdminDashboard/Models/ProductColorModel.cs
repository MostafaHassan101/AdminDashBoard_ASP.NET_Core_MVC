using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AdminDashboard.Models
{
    public class ProductColorModel
    {
        public long prodid { get;  set; }
        public long ColorId { get; set; }

        public string Name { get;  set; }

        public string HexValue { get;  set; }
    }
}
