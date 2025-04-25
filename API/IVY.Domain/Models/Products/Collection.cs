using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models
{   

    public class Collection
    {   
        [Key]
        public int Collection__Id { get; set; }
        public int Collection__Status { get; set; } = (int)ProductStatus.Releasing;
        public required string? Collection__Name { get; set; }
        public virtual ICollection<ProductCollection>? ProductCollections { get; set; }
    }
}