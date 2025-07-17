

using IVY.Domain.Models.Products;

namespace IVY.Application.DTOs;
public class AddCartDTO
{       
        public string User__Id { get; set; }
        public int CartItem__Id { get; set; }
        public int CartItem__Quantity {get;set;}
        public int ProductSubColor__Id { get; set; }//FK
        public string Size__Name { get; set; }//FK

}


    public class GetCartItemDTO
    {
        public int CartItem__Id { get; set; }
        // public SubColorGetDTO SubColorGetDTO {get;set;}
        public string? Product__Name {get;set;}
        public string? Image {get;set;}
        public int CartItem__Quantity {get;set;}
        public string CartItem__CreatedByAccountId { get; set; }
        // public int CartItem__ProductColorSizeId {get;set;}
        public ProductSubColorGetDTO? ProductSubColorGetDTO {get;set;}
        public string? CartItem__Size { get; set; }
        // public List<ProductSubColorFileGetFileDTO>? ProductSubColorFileGetFileDTO {get;set;}
        public bool? CartItem__IsSale {get;set;}=true;
        public string? CartItem__Message {get;set;}
    }
    public class UpdateCartItemDTO
    {
        public int CartItem__Id { get; set; }
        public int CartItem__Quantity { get; set; }
        public int CartItem__CreatedByAccountId { get; set; }
    }
