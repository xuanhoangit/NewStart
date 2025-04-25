

namespace IVY.Application.DTOs.Filters
{   
 
    public class OrderFilter
    {
        public int? Order__Status {get;set;}
        public int? Account__Id { get; set; }
        public int? Payment__Status {get;set;}
        public RangePrice? RangePrice {get;set;}
        public RangeDateTime? RangeDateTime {get;set;}
        public int SortBy { get; set; }= (int)Sort.Default;
    }
}