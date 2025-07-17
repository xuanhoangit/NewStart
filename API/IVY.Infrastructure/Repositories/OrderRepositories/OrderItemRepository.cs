using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository.Orders;
using IVY.Domain.Enums;
using IVY.Domain.Models.GHN;
using IVY.Domain.Models.Orders;
using IVY.Infrastructure.Data;
using IVY.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace SneakerAPI.Infrastructure.Repositories.OrderRepositories;
public class OrderItemRepository :Repository<OrderItem> ,IOrderItemRepository
{
    private readonly IVYDbContext db;

    public OrderItemRepository(IVYDbContext db):base(db)
    {
        this.db = db;
    }
    public async Task<GetOrderItemDTO> GetOrderItem(int orderItem_id){
        var query= from orderItem in db.OrderItems 
            join psc in db.ProductSubColors on orderItem.OrderItem__ProductSubColorId equals psc.ProductSubColor__Id
            // join sc in db.SubColors on psc.ProductSubColor__SubColorId equals sc.SubColor__Id
            join file in db.ProductSubColorFiles on psc.ProductSubColor__Id equals file.ProductSubColorFile__ProductSubColorId
            // into fileGroup
            join product in db.Products on psc.ProductSubColor__ProductId equals product.Product__Id
            join subcolor in db.SubColors on psc.ProductSubColor__SubColorId equals subcolor.SubColor__Id
            where orderItem.OrderItem__Id==orderItem_id 
                        select new GetOrderItemDTO {
                          OrderItem__Id = orderItem.OrderItem__Id,
                       
                        Product__Name=product.Product__Name,
                        // OrderItem__Quantity=orderItem.OrderItem__Quantity,
                        // OrderItem__ProductColorSizeId=pcs.ProductColorSize__Id,
                        // OrderItem__CreatedByAccountId = orderItem.OrderItem__CreatedByCustomerId,
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
                        OrderItem__Size =orderItem.OrderItem__Size,
                        Image = file.ProductSubColorFile__Name,
                        OrderItem__IsSale=psc.ProductSubColor__Status==(int)ProductStatus.Releasing,
                        OrderItem__Message=psc.ProductSubColor__Status!=(int)ProductStatus.Releasing?"Không còn bán":""

            };
        return await query.FirstOrDefaultAsync();
    }
    public async Task<List<GetOrderItemDTO>> GetOrderItems(int order_id){
                    var query= from orderItem in db.OrderItems 
            join psc in db.ProductSubColors on orderItem.OrderItem__ProductSubColorId equals psc.ProductSubColor__Id
            // join sc in db.SubColors on psc.ProductSubColor__SubColorId equals sc.SubColor__Id
            join file in db.ProductSubColorFiles on psc.ProductSubColor__Id equals file.ProductSubColorFile__ProductSubColorId
            // into fileGroup
            join product in db.Products on psc.ProductSubColor__ProductId equals product.Product__Id
            join subcolor in db.SubColors on psc.ProductSubColor__SubColorId equals subcolor.SubColor__Id
            where orderItem.OrderItem__OrderId==order_id 
                        select new GetOrderItemDTO {
                          OrderItem__Id = orderItem.OrderItem__Id,
                       
                        Product__Name=product.Product__Name,
                        // OrderItem__Quantity=orderItem.OrderItem__Quantity,
                        // OrderItem__ProductColorSizeId=pcs.ProductColorSize__Id,
                        // OrderItem__CreatedByAccountId = orderItem.OrderItem__CreatedByCustomerId,
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
                        OrderItem__Size =orderItem.OrderItem__Size,
                        Image = file.ProductSubColorFile__Name,
                        OrderItem__IsSale=psc.ProductSubColor__Status==(int)ProductStatus.Releasing,
                        OrderItem__Message=psc.ProductSubColor__Status!=(int)ProductStatus.Releasing?"Không còn bán":""

            };
        return await query.ToListAsync();
        }

    public async Task<List<ItemDto>> GetItemDTO(int order_id)
    {
            var query= from orderItem in db.OrderItems 
            // join pcs in db.ProductColorSizes on orderItem.OrderItem__ProductColorSizeId equals pcs.ProductColorSize__Id
            join psc in db.ProductSubColors on orderItem.OrderItem__ProductSubColorId equals psc.ProductSubColor__Id
            // join size in db.Sizes on pcs.ProductColorSize__SizeId equals size.Size__Id 
            join product in db.Products on psc.ProductSubColor__ProductId equals product.Product__Id
            join subcolor in db.SubColors on psc.ProductSubColor__SubColorId equals subcolor.SubColor__Id
            where orderItem.OrderItem__OrderId==order_id 
                        select new ItemDto {
                        Name=product.Product__Name+" - "+subcolor.SubColor__Name + " - "+orderItem.OrderItem__Size,
                        Price=(int)psc.ProductSubColor__Price,
                        Quantity=orderItem.OrderItem__Quantity,
                        Weight=500,
                        Length=25,
                        Width=10,
                        Height=10,

            };
        return await query.ToListAsync();
    }
}