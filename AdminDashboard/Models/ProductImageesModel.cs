namespace AdminDashboard.Models
{
    public class ProductImageesModel
    {
        public long Id { get; set; }

        public List<IFormFile> ImagePaths { get; set; }
        public IFormFile ImagePath { get; set; }
        public long ProductId { get; set; }

    }
}
