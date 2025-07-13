
using AutoMapper;
using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Enums;
using IVY.Domain.Libs;
using IVY.Domain.Models.Products;



namespace IVY.Application.Services.Products;

public class ProductSubColorService : IProductSubColorService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProductSubColorService(IUnitOfWork uow,IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public ProductSubColorGetDTO GetDTO(int psc_Id)
    {
        var data = _uow.ProductSubColor.GetFirstOrDefault(x => x.ProductSubColor__Id == psc_Id, "SubColor");
        var dto=_mapper.Map<ProductSubColorGetDTO>(data);
        dto.SubColorGetDTO = _mapper.Map<SubColorGetDTO>(data.SubColor);
        return dto;
    }
    public Result<ProductSubColorGetHomeShowDTO> UpdateProductSubColor(ProductSubColorGetDTO productSubColorDTO)
    {

        var checkExist = _uow.ProductSubColor.FirstOrDefault(x =>
        x.ProductSubColor__ProductId == productSubColorDTO.ProductSubColor__ProductId
        && x.ProductSubColor__SubColorId == productSubColorDTO.ProductSubColor__SubColorId
        && x.ProductSubColor__Status != (int)ProductStatus.Deleted);

        if (checkExist != null && checkExist.ProductSubColor__Id != productSubColorDTO.ProductSubColor__Id)
        {
            return Result<ProductSubColorGetHomeShowDTO>.Failure(ResultStatus.Conflict);
        }
        var productSubColor = _uow.ProductSubColor.Get(productSubColorDTO.ProductSubColor__Id);
         _mapper.Map(productSubColorDTO,productSubColor);
        productSubColor.ProductSubColor__UpdateAt = DateTime.UtcNow;
        var result = _uow.ProductSubColor.Update(productSubColor);
      
        var data = _mapper.Map<ProductSubColorGetHomeShowDTO>(productSubColor);
        return result ? Result<ProductSubColorGetHomeShowDTO>.Success(data)
        : Result<ProductSubColorGetHomeShowDTO>.Failure(ResultStatus.InternalError);

    }
    //pass
    public async Task<Result<ProductSubColorGetHomeShowDTO>> AddProductSubColor (ProductSubColorFormAddDTO  productSubColorDTO){
            var productSubColor=_uow.ProductSubColor.GetFirstOrDefault(x=>
            x.ProductSubColor__ProductId==productSubColorDTO.ProductSubColor__ProductId
            && x.ProductSubColor__SubColorId==productSubColorDTO.ProductSubColor__SubColorId,"SubColor");

        if (productSubColor == null)
        {
            var newProductSubColor = new ProductSubColor
            {
                ProductSubColor__ProductId = productSubColorDTO.ProductSubColor__ProductId,
                ProductSubColor__SubColorId = productSubColorDTO.ProductSubColor__SubColorId,
                ProductSubColor__Price = productSubColorDTO.ProductSubColor__Price,
                ProductSubColor__Discount = productSubColorDTO.ProductSubColor__Discount,
                ProductSubColor__CreateAt = DateTime.UtcNow,
                ProductSubColor__Status = (int)ProductStatus.NotComplete,
                ProductSubColor__OutfitKey = Guid.NewGuid().ToString()

            };
            var result = await _uow.ProductSubColor.AddAsync(newProductSubColor);
            var size = new Size
            {
                Size__ProductSubColorId = newProductSubColor.ProductSubColor__Id,
                Size__L = 0,
                Size__S = 0,
                Size__M = 0,
                Size__XL = 0,
                Size__XXl = 0,
            };
            _uow.Size.Add(size);
            var data = _mapper.Map<ProductSubColorGetHomeShowDTO>(newProductSubColor);
            data.SizeDTO = _mapper.Map<SizeDTO>(size);
            data.SubColorGetDTO =_mapper.Map<SubColorGetDTO>(_uow.SubColor.Get(newProductSubColor.ProductSubColor__SubColorId));
            
            return result ? Result<ProductSubColorGetHomeShowDTO>.Created(data)
                : Result<ProductSubColorGetHomeShowDTO>.Failure(ResultStatus.InternalError);
        }
            if(productSubColor.ProductSubColor__Status==(int)ProductStatus.Deleted){
                productSubColor.ProductSubColor__Price=productSubColorDTO.ProductSubColor__Price;
                productSubColor.ProductSubColor__Discount=productSubColorDTO.ProductSubColor__Discount;
                productSubColor.ProductSubColor__CreateAt=DateTime.UtcNow;
                productSubColor.ProductSubColor__Status=(int)ProductStatus.NotComplete;
                productSubColor.ProductSubColor__OutfitKey = Guid.NewGuid().ToString();
                
                var result=_uow.ProductSubColor.Update(productSubColor);
                 
                var data = _mapper.Map<ProductSubColorGetHomeShowDTO>(productSubColor);
            var size = _uow.Size.GetFirstOrDefault(x => x.Size__ProductSubColorId == productSubColor.ProductSubColor__Id);
            data.SizeDTO = _mapper.Map<SizeDTO>(size);
            data.SubColorGetDTO =_mapper.Map<SubColorGetDTO>(_uow.SubColor.Get(productSubColor.ProductSubColor__SubColorId));

            return result ? Result<ProductSubColorGetHomeShowDTO>.Success(data)
                : Result<ProductSubColorGetHomeShowDTO>.Failure(ResultStatus.InternalError);
            }
            return Result<ProductSubColorGetHomeShowDTO>.Failure(ResultStatus.Conflict);
    }
    public bool Remove(int psc_Id)
    {
        var psc = _uow.ProductSubColor.Get(psc_Id);
        if (psc != null)
        {
            psc.ProductSubColor__Status = (int)ProductStatus.Deleted;
            return _uow.ProductSubColor.Update(psc);
        }
        return false;
    }
}