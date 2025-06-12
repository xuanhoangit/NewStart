using System.ComponentModel.DataAnnotations;


namespace IVY.Domain.Models.Products
{   

    public class Category
    {   
        [Key]
        public int Category__Id { get; set; }
        public required string Category__Name { get; set; }
        public required int Category__Type { get; set; }
        public required int Category__Status { get; set; }
        public virtual ICollection<SubCategory>? SubCategories { get; set; }
    }
}