namespace IVY.Application.DTOs.Filters;
    public enum TypeOfCategory{
        Common=1,
        AlternatingEvents=2
    }
    public class RangePrice{
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
    public enum SaleMaketting{
        BestSeller,
        NewArrival
    }

    public enum Sort
    {
        Default = 0,
        DateDecrease,
        DateIncrease,
        ZA,
        AZ,
        PriceDecrease,
        PriceIncrease
    }
    public class RangeDateTime{
        public DateTime? From {get;set;}
        public DateTime? To {get;set;} 
    }