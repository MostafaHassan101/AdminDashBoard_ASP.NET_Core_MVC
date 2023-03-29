namespace Domain.Entities
{
    public class ProductImage
    {
        public long Id { get; set; }

        public string ImagePath { get; set; }

        //public long ProductId { get; set; }

        public virtual Product Product { get; set; }

    }
}
