namespace AdminDashboard.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int? parentId { get; set; }
        public virtual CategoryModel? ParentCategory { get; set; }
        public virtual ICollection<CategoryModel>? SubCategories { get; set; }
    }
}
