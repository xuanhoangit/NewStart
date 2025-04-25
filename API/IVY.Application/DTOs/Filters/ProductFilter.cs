namespace IVY.Application.DTOs.Filters
{   

    public class ProductFilter
    {   
        
        public string? SearchString { get; set; }
        public RangeDateTime FromDateTo { get; set; }
        public int[]? ColorIds { get; set; }
        public RangePrice? RangePrice { get; set; }
        public bool OrderByDatetime {get;set;}
    }
}