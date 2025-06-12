using AutoMapper;
using IVY.Application.DTOs;
using IVY.Domain.Models.Products;



public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductFormAddDTO, Product>();
        CreateMap<Product, ProductGetWithProductHomeShowDTO>().ReverseMap();
        CreateMap<SubColor, SubColorGetDTO>();

        CreateMap<ProductSubColor, ProductSubColorGetHomeShowDTO>().ReverseMap();
        CreateMap<ProductSubColor, ProductSubColorGetDTO>().ReverseMap();
        CreateMap<Size, SizeDTO>().ReverseMap();
       
        // CreateMap<StaffInfo,StaffInfoDTO>().ReverseMap();
        // CreateMap<CustomerInfo,CustomerInfoDTO>().ReverseMap();
        // CreateMap<Address,AddressDTO>().ReverseMap();
        // CreateMap<CartItem,AddCartDTO>().ReverseMap();
        // CreateMap<Favorite,AddFavoriteDTO>().ReverseMap();
    }
}
