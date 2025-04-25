using System.ComponentModel.DataAnnotations;


namespace IVY.Domain.Models
{   

    public class Category
    {   
        [Key]
        public int Category__Id { get; set; }
        public required string Category__Name { get; set; }
        public virtual ICollection<ProductCategory>? Products { get; set; }
    }
}