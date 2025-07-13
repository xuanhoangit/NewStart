
using AutoMapper;
using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Enums;
using IVY.Domain.Libs;
using IVY.Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


namespace IVY.Application.Services.Products;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _uow;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork uow, IMemoryCache memoryCache,IMapper mapper)
    {
        _uow = uow;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }
    public Result<ProductGetWithProductHomeShowDTO> Update(ProductFormUpdateDTO productFormUpdateDTO)
    {
        productFormUpdateDTO.Product__Name = StringValid.ConvertToValidString(productFormUpdateDTO.Product__Name);
        //check subcategory is exist
        var subCateIsExist = _uow.SubCategory.Find(x => productFormUpdateDTO.SubCategoryIds.Contains(x.SubCategory__Id));

        if (subCateIsExist.Count() != productFormUpdateDTO.SubCategoryIds.Count())
        {
            return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.BadRequest);
        }
        //check collection is exist
        var collectionIsExist = _uow.Collection.Find(x => productFormUpdateDTO.CollectionIds.Contains(x.Collection__Id));
        if (collectionIsExist.Count() != productFormUpdateDTO.CollectionIds.Count())
        {
            return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.BadRequest);
        }
        // check product is exist
        var product = _uow.Product.GetFirstOrDefault(x => x.Product__Name == productFormUpdateDTO.Product__Name && x.Product__Id !=productFormUpdateDTO.Product__Id
        && x.Product__Status!=(int)ProductStatus.Deleted);
        if (product != null)
        {
            return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.Conflict);
        }
        var updateProduct = _uow.Product.Get(productFormUpdateDTO.Product__Id);
        if (updateProduct != null&&updateProduct.Product__Status!=(int)ProductStatus.Deleted)
        {
            updateProduct.Product__Name = productFormUpdateDTO.Product__Name;
            updateProduct.Product__SeasonId = productFormUpdateDTO.Product__SeasonId;
            updateProduct.Product__CreateAt = DateTime.UtcNow;
            updateProduct.Product__Status = (int)ProductStatus.Releasing;
            // updateProduct.Product__Sold = 0;
            
            var result = _uow.Product.Update(updateProduct);
            if (result)
            {
                AddProductSubCategoryAndProductCollection(
                productFormUpdateDTO.CollectionIds, productFormUpdateDTO.SubCategoryIds, updateProduct.Product__Id);
                var data = _mapper.Map<ProductGetWithProductHomeShowDTO>(updateProduct);
                return Result<ProductGetWithProductHomeShowDTO>.Success(data);
            }
            return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.InternalError);
        }
        
        return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.Conflict);
       
    }
    public List<dynamic> Search(string text)
    {
        text = StringValid.ConvertToValidString(text);
        var result = _uow.Product.GetAll(x => x.Product__Name.Contains(text) && x.Product__Status != (int)ProductStatus.Deleted)
        .Select(x => new
        {
            x.Product__Name,
            x.Product__Id
        })
        .Cast<dynamic>()
        .ToList();
        return result;
    }
    private void AddProductSubCategoryAndProductCollection(int[] CollectionIds, int[] SubCategoryIds, int productId)
    {
        System.Console.WriteLine("Pridyct ID: " + productId);
        foreach (var collectionId in CollectionIds)
        {
            var productCollection = _uow.ProductCollection.Find(x => x.ProductCollection__ProductId == productId && x.ProductCollection__CollectionId == collectionId);
            if (productCollection != null)
                continue;
            _uow.ProductCollection.Add(new ProductCollection
            {
                ProductCollection__CollectionId = collectionId,
                ProductCollection__ProductId = productId
            });
        }
        foreach (var subCateId in SubCategoryIds)
        {
            var productSCategory = _uow.ProductSubCategory.Find(x => x.ProductSubCategory__ProductId == productId && x.ProductSubCategory__SubCategoryId == subCateId);
            if (productSCategory != null)
                continue;
            _uow.ProductSubCategory.Add(new ProductSubCategory
            {
                ProductSubCategory__SubCategoryId = subCateId,
                ProductSubCategory__ProductId = productId,
            });
        }
    }
    public async Task<Result<ProductGetWithProductHomeShowDTO>> Add(ProductFormAddDTO productFormAddDTO)
    {
        productFormAddDTO.Product__Name = StringValid.ConvertToValidString(productFormAddDTO.Product__Name);
        //check subcategory is exist
        var subCateIsExist = _uow.SubCategory.Find(x => productFormAddDTO.SubCategoryIds.Contains(x.SubCategory__Id));

        if (subCateIsExist.Count() != productFormAddDTO.SubCategoryIds.Count())
        {
            return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.BadRequest);
        }
        //check collection is exist
        var collectionIsExist = _uow.Collection.Find(x => productFormAddDTO.CollectionIds.Contains(x.Collection__Id));
        if (collectionIsExist.Count() != productFormAddDTO.CollectionIds.Count())
        {
            return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.BadRequest);
        }
        // check product is exist
        var product = _uow.Product.GetFirstOrDefault(x => x.Product__Name == productFormAddDTO.Product__Name);
        if (product == null)
        {
            var newProduct = new Product
            {
                Product__Name = productFormAddDTO.Product__Name,
                Product__SeasonId = productFormAddDTO.Product__SeasonId,
                Product__CreateAt = DateTime.UtcNow,
                Product__Status = (int)ProductStatus.NotComplete,
                Product__Sold = 0,
            };
            var result = await _uow.Product.AddAsync(newProduct);
            if (result)
            {
                AddProductSubCategoryAndProductCollection(
                productFormAddDTO.CollectionIds, productFormAddDTO.SubCategoryIds, newProduct.Product__Id);
                var data = _mapper.Map<ProductGetWithProductHomeShowDTO>(newProduct);
                return Result<ProductGetWithProductHomeShowDTO>.Created(data);
            }
            return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.InternalError);
        }
        // nếu product đã tồn tại nhưng status = deleted
        // => active sản phẩm
        if (product.Product__Status == (int)ProductStatus.Deleted)
        {
            product.Product__Status = (int)ProductStatus.NotComplete;
            product.Product__Sold = 0;
            product.Product__CreateAt = DateTime.UtcNow;
            product.Product__SeasonId = productFormAddDTO.Product__SeasonId;
            //bổ sung thêm thông tin
            var result = _uow.Product.Update(product);
            if (result)
            {
                AddProductSubCategoryAndProductCollection(
                    productFormAddDTO.CollectionIds, productFormAddDTO.SubCategoryIds, product.Product__Id);
                var data = _mapper.Map<ProductGetWithProductHomeShowDTO>(product);
                return Result<ProductGetWithProductHomeShowDTO>.Success(data);
            }
            return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.InternalError); ;
        }
        return Result<ProductGetWithProductHomeShowDTO>.Failure(ResultStatus.Conflict);
    }

    public async Task<dynamic> GetProductByFilter(ProductFilter filter)
    {


        var products = await _uow.Product.GetAllDTO(filter);
        return new
        {
            products,
            count = _uow.Product.GetAll(x=>x.Product__Status!=(int)ProductStatus.Deleted).Count()
        };
    }
    public async Task<dynamic> GetProducts(int Page)
    {

        var cacheKey = "page_" + Page;

        if (!_memoryCache.TryGetValue(cacheKey, out dynamic data))
        {

            var products = await _uow.Product.GetAllDTO(Page);

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            _memoryCache.Set(cacheKey, new
            {
                products,
                count = _uow.Product.GetAll().Count()
            }, cacheOptions);
        }
        return data;

    }
    public bool Remove(int product__Id)
    {
        var product = _uow.Product.Get(product__Id);
        if (product == null)
        {
            return false;
        }
        product.Product__Status = (int)ProductStatus.Deleted;
        _uow.Product.Update(product);
        return true;
    }
    
  
}