using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository.Orders;
using IVY.Domain.Enums;
using IVY.Domain.Models.Orders;
using IVY.Infrastructure.Data;
using IVY.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace SneakerAPI.Infrastructure.Repositories.OrderRepositories
{
    public class CartItemRepository: Repository<CartItem>,ICartItemRepository
    {
        private readonly IVYDbContext db;

        public CartItemRepository(IVYDbContext _db):base(_db)
        {
            db = _db;
        }

        public async Task<List<GetCartItemDTO>> GetCartItem(string user_id){
            var query= from cart in db.CartItems 
            join psc in db.ProductSubColors on cart.CartItem__ProductSubColorId equals psc.ProductSubColor__Id
            // join sc in db.SubColors on psc.ProductSubColor__SubColorId equals sc.SubColor__Id
            join file in db.ProductSubColorFiles on psc.ProductSubColor__Id equals file.ProductSubColorFile__ProductSubColorId
            // into fileGroup
            join product in db.Products on psc.ProductSubColor__ProductId equals product.Product__Id
            join subcolor in db.SubColors on psc.ProductSubColor__SubColorId equals subcolor.SubColor__Id
            where cart.CartItem__CreatedByCustomerId==Guid.Parse(user_id) 
            select new GetCartItemDTO {
                        CartItem__Id = cart.CartItem__Id,
                       
                        Product__Name=product.Product__Name,
                        CartItem__Quantity=cart.CartItem__Quantity,
                        // CartItem__ProductColorSizeId=pcs.ProductColorSize__Id,
                        CartItem__CreatedByAccountId = cart.CartItem__CreatedByCustomerId.ToString(),
                        ProductSubColorGetDTO = new ProductSubColorGetDTO
                        {
                            ProductSubColor__Id = psc.ProductSubColor__Id,
                            ProductSubColor__Price = psc.ProductSubColor__Price,
                            ProductSubColor__Discount = psc.ProductSubColor__Discount,
                            SubColorGetDTO=new SubColorGetDTO
                            {
                                SubColor__Name = subcolor.SubColor__Name,
                                SubColor__Id = subcolor.SubColor__Id,
                                SubColor__Image = subcolor.SubColor__Image, 
                            },
                        },
                        CartItem__Size =cart.CartItem__Size,
                        Image = file.ProductSubColorFile__Name,
                        CartItem__IsSale=psc.ProductSubColor__Status==(int)ProductStatus.Releasing,
                        CartItem__Message=psc.ProductSubColor__Status!=(int)ProductStatus.Releasing?"Không còn bán":""
            };
            // if(cartItem_Ids!=null){
            //      query=query.Where(x=>cartItem_Ids.Contains(x.CartItem__Id));
            // }
        return await query.ToListAsync();
        }
    }
}