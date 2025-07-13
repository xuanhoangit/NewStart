
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Enums;
using IVY.Domain.Libs;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductRepository :Repository<Product> ,IProductRepository
{
    private readonly IVYDbContext _db;
    public ProductRepository(IVYDbContext db):base(db)
    {
        _db =db;
    }
    public async Task<bool> AddProduct(ProductFormAddDTO addDTO){
    using (var transaction=_context.Database.BeginTransaction()){
        try
        {
            var product= new Product{
                Product__Name=addDTO.Product__Name,
                Product__SeasonId=addDTO.Product__SeasonId,
                Product__CreateAt=DateTime.UtcNow,
                Product__Status=(int)ProductStatus.Releasing,
                Product__Sold=0,
                // Product__ProductLineId=addDTO.ProductLine__Id
            };
            await _db.Products.AddAsync(product);
            foreach (var collectionId in addDTO.CollectionIds)
            {
                _db.ProductCollections.Add(new ProductCollection{
                    ProductCollection__CollectionId=collectionId,
                    ProductCollection__ProductId=product.Product__Id
                });
            }
            foreach (var subCategoryId in addDTO.SubCategoryIds)
            {
                _db.ProductSubCategories.Add(new ProductSubCategory{
                    ProductSubCategory__SubCategoryId=subCategoryId,
                    ProductSubCategory__ProductId=product.Product__Id,
                });
            }
            _db.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch (System.Exception)
        {
            transaction.Rollback();
            return false;
        }
    }
}
    // Product__SeasonName = EnumHelper.GetEnumDescription((Season)p.Product__Status),
   
    private IQueryable<Product> GetQuery(ProductFilter filter) {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = _dbSet
        .Include(p => p.ProductSubColors)
            .ThenInclude(psc => psc.Size) // ✅ Đến đây là đủ nếu chỉ cần lấy Size
        .Include(p => p.ProductSubColors)
            .ThenInclude(psc => psc.SubColor)
                .ThenInclude(sc => sc.ColorSubColors)
                    .ThenInclude(csc => csc.Color)
        .Include(p => p.ProductSubCategories)
            .ThenInclude(psc => psc.SubCategory)
        .AsQueryable();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        query = query.Where(p => p.Product__Status != (int)ProductStatus.Deleted);
        // Lọc theo tên sản phẩm
        if (!string.IsNullOrWhiteSpace(filter.SearchString))
        {
            query = query.Where(p => p.Product__Name.Contains(filter.SearchString));
        }
    //     if (filter.RoleRequest == RolesName.Staff)
    //     {
    //         query = query.Where(p =>
    //         p.Product__Status == (int)ProductStatus.Releasing &&
    //         p.ProductSubColors.Any(x => x.ProductSubColor__Status == (int)ProductStatus.Releasing)
    // );
    //     }
        if (filter.Sizes != null && filter.Sizes.Any())
        {
            query = query.Where(p => p.ProductSubColors.Any(psc =>
                psc.Size != null &&
                (
                    (filter.Sizes.Contains("S") && psc.Size.Size__S > 0) ||
                    (filter.Sizes.Contains("M") && psc.Size.Size__M > 0) ||
                    (filter.Sizes.Contains("L") && psc.Size.Size__L > 0) ||
                    (filter.Sizes.Contains("XL") && psc.Size.Size__XL > 0) ||
                    (filter.Sizes.Contains("XXL") && psc.Size.Size__XXl > 0)
                )
            ));
        }
        // Lọc theo ngày tạo
        if (filter.FromDateTo?.From != null)
        {
            query = query.Where(p => p.Product__CreateAt >= filter.FromDateTo.From.Value);
        }
        if (filter.FromDateTo?.To != null)
        {
            query = query.Where(p => p.Product__CreateAt <= filter.FromDateTo.To.Value);
        }

        // Lọc theo SubCategory
        if (filter.SubCategory__Id > 0)
        {
            query = query.Where(p => p.ProductSubCategories.Any(psc => psc.ProductSubCategory__SubCategoryId == filter.SubCategory__Id));
        }

        // Lọc theo ColorId (qua ColorSubColor -> SubColor -> ProductSubColor)
        if (filter.ColorIds != null && filter.ColorIds.Any())
        {
            query = query.Where(p =>
                p.ProductSubColors.Any(psc =>
                    psc.SubColor != null &&
                    psc.SubColor.ColorSubColors.Any(csc =>
                        filter.ColorIds.Contains(csc.ColorSubColor__ColorId)
                    )
                )
            );
        }

        // Lọc theo giá
        if (filter.RangePrice?.MinPrice != null)
        {
            query = query.Where(p => p.ProductSubColors.Any(psc => psc.ProductSubColor__Price >= filter.RangePrice.MinPrice.Value));
        }
        if (filter.RangePrice?.MaxPrice != null)
        {
            query = query.Where(p => p.ProductSubColors.Any(psc => psc.ProductSubColor__Price <= filter.RangePrice.MaxPrice.Value));
        }

        // Sắp xếp theo ngày tạo
        if (filter.OrderByDatetime)
        {
            query = query.OrderByDescending(p => p.Product__CreateAt);
        }

        // Thực thi truy vấn
        return query;
    }
    private IQueryable<Product> GetQuery(int Page)
    {
        var query = _dbSet
       .Include(p => p.ProductSubColors)
           .ThenInclude(psc => psc.Size) // ✅ Đến đây là đủ nếu chỉ cần lấy Size
       .Include(p => p.ProductSubColors)
           .ThenInclude(psc => psc.SubColor)
               .ThenInclude(sc => sc.ColorSubColors)
                   .ThenInclude(csc => csc.Color)
       .Include(p => p.ProductSubCategories)
           .ThenInclude(psc => psc.SubCategory)
        .AsSplitQuery();//    .AsQueryable();
        return query;
     }
    public async Task<List<ProductGetWithProductHomeShowDTO>> GetAllDTO( ProductFilter filter)
    {   
        var result = GetQuery(filter);
    
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    return await result?.OrderByDescending(x=>x.Product__Id).Skip((filter.Page-1)*16).Take(16).Select(p => new ProductGetWithProductHomeShowDTO
    {
        Product__Id = p.Product__Id,
        Product__Name = p.Product__Name,
        Product__Status = p.Product__Status,
        Product__Sold = p.Product__Sold,
        Product__CreateAt = p.Product__CreateAt,
        // SubColorGetDTOs=p.ProductSubColors.Select(x=>x.SubColor)
        // .Select(x=>x).ToList(),
        ProductSubColorGetHomeShowDTOs = filter.RoleRequest!=RolesName.Staff?p.ProductSubColors.Where(x=>x.ProductSubColor__Status!=(int)ProductStatus.Deleted)
            .OrderBy(psc => psc.ProductSubColor__CreateAt) // hoặc theo thời gian, hoặc theo Discount tùy ý
            .Select(psc => new ProductSubColorGetHomeShowDTO
            {
                ProductSubColor__Id = psc.ProductSubColor__Id,
                ProductSubColor__Price = psc.ProductSubColor__Price,
                ProductSubColor__Discount = psc.ProductSubColor__Discount,
                ProductSubColor__CreateAt = psc.ProductSubColor__CreateAt,
                ProductSubColor__UpdateAt = psc.ProductSubColor__UpdateAt,
                ProductSubColor__OutfitKey = psc.ProductSubColor__OutfitKey,
                ProductSubColor__Status = psc.ProductSubColor__Status,
                ProductSubColor__ProductId = psc.ProductSubColor__ProductId,
                ProductSubColor__SubColorId=psc.ProductSubColor__SubColorId,
                SizeDTO = new SizeDTO
                {
                    Size__Id = psc.Size.Size__Id,
                    Size__M = psc.Size.Size__M,
                    Size__L= psc.Size.Size__L,
                    Size__S= psc.Size.Size__S,
                    Size__XL= psc.Size.Size__XL,
                    Size__XXl=psc.Size.Size__XXl
                },
                SubColorGetDTO =new SubColorGetDTO{
                    SubColor__Id = psc.SubColor.SubColor__Id,
                    SubColor__Name=psc.SubColor.SubColor__Name,
                    SubColor__Image=psc.SubColor.SubColor__Image
                },
                ProductSubColorFileGetFileDTOs=psc.ProductSubColorFile.OrderBy(x=>x.ProductSubColorFile__Index).Select(s =>new ProductSubColorFileGetFileDTO{
                    ProductSubColorFile__Id=s.ProductSubColorFile__Id,
                    ProductSubColorFile__Index=s.ProductSubColorFile__Index,
                    ProductSubColorFile__Name =s.ProductSubColorFile__Name,
                    ProductSubColorFile__ProductColorId=s.ProductSubColorFile__Id
                }).Take(2).ToList()
               
            })
            .ToList():p.ProductSubColors.Where(x=>x.ProductSubColor__Status==(int)ProductStatus.Releasing)
            .OrderBy(psc => psc.ProductSubColor__CreateAt) // hoặc theo thời gian, hoặc theo Discount tùy ý
            .Select(psc => new ProductSubColorGetHomeShowDTO
            {
                ProductSubColor__Id = psc.ProductSubColor__Id,
                ProductSubColor__Price = psc.ProductSubColor__Price,
                ProductSubColor__Discount = psc.ProductSubColor__Discount,
                ProductSubColor__CreateAt = psc.ProductSubColor__CreateAt,
                ProductSubColor__UpdateAt = psc.ProductSubColor__UpdateAt,
                ProductSubColor__OutfitKey = psc.ProductSubColor__OutfitKey,
                ProductSubColor__Status = psc.ProductSubColor__Status,
                ProductSubColor__ProductId = psc.ProductSubColor__ProductId,
                ProductSubColor__SubColorId=psc.ProductSubColor__SubColorId,
                SizeDTO = new SizeDTO
                {
                    Size__Id = psc.Size.Size__Id,
                    Size__M = psc.Size.Size__M,
                    Size__L= psc.Size.Size__L,
                    Size__S= psc.Size.Size__S,
                    Size__XL= psc.Size.Size__XL,
                    Size__XXl=psc.Size.Size__XXl
                },
                SubColorGetDTO =new SubColorGetDTO{
                    SubColor__Id = psc.SubColor.SubColor__Id,
                    SubColor__Name=psc.SubColor.SubColor__Name,
                    SubColor__Image=psc.SubColor.SubColor__Image
                },
                ProductSubColorFileGetFileDTOs=psc.ProductSubColorFile.OrderBy(x=>x.ProductSubColorFile__Index).Select(s =>new ProductSubColorFileGetFileDTO{
                    ProductSubColorFile__Id=s.ProductSubColorFile__Id,
                    ProductSubColorFile__Index=s.ProductSubColorFile__Index,
                    ProductSubColorFile__Name =s.ProductSubColorFile__Name,
                    ProductSubColorFile__ProductColorId=s.ProductSubColorFile__Id
                }).Take(2).ToList()
               
            })
            .ToList()
    })
    .ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    
    }
    public async Task<List<ProductGetWithProductHomeShowDTO>> GetAllDTO( int page)
    {   
        var result = GetQuery(page);
    
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    return await result.Skip((page-1)*16).Take(16).Select(p => new ProductGetWithProductHomeShowDTO
    {
        Product__Id = p.Product__Id,
        Product__Name = p.Product__Name,
        Product__Status = p.Product__Status,
        Product__Sold = p.Product__Sold,
        Product__CreateAt = p.Product__CreateAt,
        // SubColorGetDTOs=p.ProductSubColors.Select(x=>x.SubColor)
        // .Select(x=>x).ToList(),
        ProductSubColorGetHomeShowDTOs = p.ProductSubColors
            .OrderBy(psc => psc.ProductSubColor__CreateAt) // hoặc theo thời gian, hoặc theo Discount tùy ý
            .Select(psc => new ProductSubColorGetHomeShowDTO
            {
                ProductSubColor__Id = psc.ProductSubColor__Id,
                ProductSubColor__Price = psc.ProductSubColor__Price,
                ProductSubColor__Discount = psc.ProductSubColor__Discount,
                ProductSubColor__CreateAt = psc.ProductSubColor__CreateAt,
                ProductSubColor__UpdateAt = psc.ProductSubColor__UpdateAt,
                ProductSubColor__OutfitKey = psc.ProductSubColor__OutfitKey,
                ProductSubColor__Status = psc.ProductSubColor__Status,
                ProductSubColor__ProductId = psc.ProductSubColor__ProductId,
                ProductSubColor__SubColorId=psc.ProductSubColor__SubColorId,
                SizeDTO = new SizeDTO
                {
                    Size__Id = psc.Size.Size__Id,
                    Size__M = psc.Size.Size__M,
                    Size__L= psc.Size.Size__L,
                    Size__S= psc.Size.Size__S,
                    Size__XL= psc.Size.Size__XL,
                    Size__XXl=psc.Size.Size__XXl
                },
                SubColorGetDTO =new SubColorGetDTO{
                    SubColor__Id = psc.SubColor.SubColor__Id,
                    SubColor__Name=psc.SubColor.SubColor__Name,
                    SubColor__Image=psc.SubColor.SubColor__Image
                },
                ProductSubColorFileGetFileDTOs=psc.ProductSubColorFile.OrderBy(x=>x.ProductSubColorFile__Index).Select(s =>new ProductSubColorFileGetFileDTO{
                    ProductSubColorFile__Id=s.ProductSubColorFile__Id,
                    ProductSubColorFile__Index=s.ProductSubColorFile__Index,
                    ProductSubColorFile__Name =s.ProductSubColorFile__Name,
                    ProductSubColorFile__ProductColorId=s.ProductSubColorFile__Id
                }).Take(2).ToList()
               
            })
            .ToList()
    })
    .ToListAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    
    }


}