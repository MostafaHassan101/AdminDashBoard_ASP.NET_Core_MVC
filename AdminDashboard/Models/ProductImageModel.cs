using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Models
{
    public class ProductImageModel
    {
        public string ImagePath { get; set; }
        public long ProductId { get; set; }
    }
    
}
