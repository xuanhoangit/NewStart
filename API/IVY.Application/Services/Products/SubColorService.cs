
using AutoMapper;
using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Enums;
using IVY.Domain.Libs;
using IVY.Domain.Models.Products;


namespace IVY.Application.Services.Products;
public class SubColorService : ISubColorService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly string storageFilePath="colors";

    public SubColorService (IUnitOfWork uow,IMapper mapper){
        _uow = uow;
        _mapper = mapper;
    }
    private void AddColorSubColor(int[] colorIds,int subcolorId){
        foreach (var cid in colorIds)
        {   
            var result=_uow.ColorSubColor.GetFirstOrDefault(x=>x.ColorSubColor__SubColorId==subcolorId && x.ColorSubColor__ColorId==cid);
            if(result==null){
                _uow.ColorSubColor.Add(new ColorSubColor{
                ColorSubColor__ColorId=cid,
                ColorSubColor__SubColorId=subcolorId
            });
            }
        }
    }
    public async Task<List<SubColorGetDTO>> GetAllAsync(){
        var subColors=await _uow.SubColor.GetAllAsync();
        return subColors.OrderByDescending(x=>x.SubColor__Id).Select(x=>new SubColorGetDTO{
            SubColor__Name=x.SubColor__Name,
            SubColor__Image=x.SubColor__Image,
            SubColor__Id=x.SubColor__Id,
        }).ToList();
    }
    public async Task<Result<SubColor>> AddSubColor(SubColorAddDTO subColorDTO){
        subColorDTO.SubColor__Name=StringValid.ConvertToValidString(subColorDTO.SubColor__Name);
        var subcolor=_uow.SubColor.GetFirstOrDefault(x=>x.SubColor__Name==subColorDTO.SubColor__Name);
        var cloudinaryService=new CloudinaryService ();
        if (subcolor==null){
            var uploadResult=await cloudinaryService.UploadImageAsync(subColorDTO.SubColor__Image,storageFilePath);
            System.Console.WriteLine(uploadResult);
            var newSubcolor=new SubColor{
              SubColor__Name = subColorDTO.SubColor__Name,
              SubColor__Image=uploadResult, 
              SubColor__Status=(int)ProductStatus.Releasing
            };
            var result = _uow.SubColor.Add(newSubcolor);
            if(result){
                AddColorSubColor(subColorDTO.ColorIds,newSubcolor.SubColor__Id);
                return Result<SubColor>.Created(newSubcolor);
            }
            return Result<SubColor>.Failure(ResultStatus.InternalError);
        }
        if(subcolor.SubColor__Status==(int)ProductStatus.Deleted){
              var uploadResult=cloudinaryService.UploadImageAsync(subColorDTO.SubColor__Image,storageFilePath,subcolor.SubColor__Image);
              subcolor.SubColor__Status=(int)ProductStatus.Releasing;
              var result=_uow.SubColor.Update(subcolor);
              if(result){
                AddColorSubColor(subColorDTO.ColorIds,subcolor.SubColor__Id);
                return Result<SubColor>.Created(subcolor);
                }
             return Result<SubColor>.Failure(ResultStatus.InternalError);
        }
        return Result<SubColor>.Failure(ResultStatus.Conflict);
    }
}