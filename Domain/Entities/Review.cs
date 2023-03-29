namespace Domain.Entities
{
    public class ProductReview
    {
        
        public long Id { get; set; }
        
        public string Review { get; set; }
                        
        public User User { get; set; }

        public Product Product { get; set; }
    }
}
