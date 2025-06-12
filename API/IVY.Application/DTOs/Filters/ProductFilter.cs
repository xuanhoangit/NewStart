namespace IVY.Application.DTOs.Filters
{

    public class ProductFilter
    {

        public string? SearchString { get; set; }// lọc bằng tên sản phẩm
        public RangeDateTime? FromDateTo { get; set; }// lọc bằng thời điểm tạo sản phẩm từ ngày tháng năm đến ngày tháng năm 

        public int[]? ColorIds { get; set; } // lọc bằng Color__Id trong class Color.cs không phải bằng subcolorId
        public RangePrice? RangePrice { get; set; } //lọc bằng giá sản phẩm trong ProductSubColor__Price
        public bool OrderByDatetime { get; set; }// sắp xếp theo thời gian tạo
        public int SubCategory__Id { get; set; }// lọc bằng SubCategories
        public List<string>? Sizes { get; set; }
        public int Page { get; set; } = 1;
    }
}