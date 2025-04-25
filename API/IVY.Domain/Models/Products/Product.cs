using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models
{   
    public class Product
    {   
        [Key]
        public int Product__Id { get; set; }
        public required string Product__Name { get; set; }
        public int Product__Sold { get; set; }=0;
        public required byte Product__Status { get; set; }=(int)ProductStatus.Releasing;
    
        public required byte Product__SeasonId { get; set; }
        public DateTime Product__CreateAt { get; set; }=DateTime.UtcNow;
        public DateTime Product__UpdateAt { get; set; }
        public virtual ICollection<ProductCollection>? ProductCollections{ get; set; }
        public virtual ICollection<ProductCategory>? ProductColors { get; set; }
 
    }
}