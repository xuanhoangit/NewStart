using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IVY.Domain.Models.Products
{   

    public class SubCategory
    {   
        [Key]
        public int SubCategory__Id { get; set; }
        public required string SubCategory__Name { get; set; }
        public required int SubCategory__Status { get; set; }
        public  int SubCategory__CategoryId {get;set;}
        [ForeignKey("SubCategory__CategoryId")]
        public Category? Category { get; set; }
        public virtual ICollection<ProductSubCategory>? ProductSubCategories { get; set; }
    }
}