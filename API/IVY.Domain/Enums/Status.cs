using System.ComponentModel;

namespace IVY.Domain.Enums
{
    public enum ProductStatus
    {   
        Deleted = 0,
        [Description("đang phát hành")]
        Releasing = 1,
        [Description("Đã ngưng phát hành")]
        Discontinued = 2,
    }
    public enum Season
    {   
        [Description("Thu - Đông")]
        AUTUMN_WINTER = 1,
        [Description("Xuân - Hè")]
        SPRING_SUMMER = 2,
        [Description("4 mùa")]
        YEAR_ROUND = 3

    }
}