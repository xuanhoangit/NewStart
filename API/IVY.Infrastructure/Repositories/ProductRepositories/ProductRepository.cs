
using System.Linq;
using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Application.DTOs.Products;
using IVY.Domain.Libs;
using IVY.Domain.Models;
using IVY.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductRepository :Repository<Product> ,IProductRepository
{
    private readonly IVYDbContext _db;
    public ProductRepository(IVYDbContext db):base(db)
    {
        _db =db;
    }

    public IQueryable<ProductGetWithProductHomeShowDTO> Query(ProductFilter filter)
    {
       var query = from p in _context.Products
            join pct in _context.ProductCollections on p.Product__Id equals pct.ProductCollection__ProductId
            join ct in _context.Collections on pct.ProductCollection__CollectionId equals ct.Collection__Id
            where 
                (string.IsNullOrEmpty(filter.SearchString) || p.Product__Name.Contains(filter.SearchString)) &&
                (filter.FromDateTo == null || 
                    ((filter.FromDateTo.From == null || p.Product__CreateAt >= filter.FromDateTo.From) &&
                     (filter.FromDateTo.To == null || p.Product__CreateAt <= filter.FromDateTo.To)))
            select new ProductGetWithProductHomeShowDTO
            {
                Collection__Name = ct.Collection__Name,
                Product__Id = p.Product__Id,
                Product__Name = p.Product__Name,
                Product__Status = p.Product__Status,
                Product__SeasonName = EnumHelper.GetEnumDescription((Season)p.Product__Status),
                Product__CreateAt = p.Product__CreateAt,

                ProductColorGetHomeShowDTO = (
                    from pc in _context.ProductColors
                    join file in _context.ProductColorFiles on pc.ProductColor__Id equals file.ProductColor__Id
                    join c in _context.Colors on pc.ProductColor__ColorId equals c.Color__Id
                    join s in _context.Sizes on pc.ProductColor__Id equals s.Size__ProductColorId
                    where 
                        p.Product__Id == pc.ProductColor__ProductId &&
                        (filter.ColorIds == null || filter.ColorIds.Contains(pc.ProductColor__ColorId)) &&
                        (filter.RangePrice == null ||
                            ((filter.RangePrice.MinPrice == null || pc.ProductColor__Price >= filter.RangePrice.MinPrice) &&
                             (filter.RangePrice.MaxPrice == null || pc.ProductColor__Price <= filter.RangePrice.MaxPrice)))
                    orderby Guid.NewGuid()
                    select new ProductColorGetHomeShowDTO
                    {
                        ProductColor__Id = pc.ProductColor__Id,
                        ProductColor__Price = pc.ProductColor__Price,
                        ProductColor__Discount = pc.ProductColor__Discount,
                        ProductColor__CreateAt = pc.ProductColor__CreateAt,
                        ProductColor__UpdateAt = pc.ProductColor__UpdateAt,
                        ProductColor__Status = pc.ProductColor__Status,
                        ProductColor__Name = pc.ProductColor__Name,
                        ProductColorFile__Name = file.ProductFile__Name,
                        ProductColor__ColorId = pc.ProductColor__ColorId,
                        ProductColor__ProductId = pc.ProductColor__ProductId
                    }
                ).FirstOrDefault()
            };

        if (filter.OrderByDatetime)
        {
            query = query.OrderByDescending(p => p.Product__CreateAt);
        }
        else
        {
            query = query.OrderBy(p => p.Product__CreateAt);
        }

        return query;
    }
    public async Task<List<ProductGetWithProductHomeShowDTO>> GetAllDTO( ProductFilter filter)
    {   
 
        return await Query(filter).ToListAsync();
    }

    public async Task<ProductGetWithProductHomeShowDTO> GetDTO( ProductFilter filter)
    {
        return await Query(filter).FirstOrDefaultAsync();
    }

}