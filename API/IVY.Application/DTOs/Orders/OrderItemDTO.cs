

namespace IVY.Application.DTOs
{
    public class GetOrderItemDTO
    {
        public int OrderItem__Id { get; set; }
        public string? Color {get;set;}
        public string? Image {get;set;}
        public string? Product__Name {get;set;}
        public string OrderItem__CreatedByAccountId { get; set; }
        public ProductSubColorGetDTO? ProductSubColorGetDTO {get;set;}
        public string? OrderItem__Size { get; set; }
        // public List<ProductSubColorFileGetFileDTO>? ProductSubColorFileGetFileDTO {get;set;}
        public bool? OrderItem__IsSale {get;set;}=true;
        public string? OrderItem__Message {get;set;}
    }
}