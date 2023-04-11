namespace Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAr { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

      
    }
}
