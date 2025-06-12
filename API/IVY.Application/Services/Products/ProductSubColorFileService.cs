using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Models.Products;

namespace IVY.Application.Services.Products;

public class ProductSubColorFileService : IProductSubColorFileService
{
    private readonly IUnitOfWork _uow;
    private readonly CloudinaryService _cloudinaryService;

    public ProductSubColorFileService(IUnitOfWork uow)
    {
        _uow = uow;
        _cloudinaryService=new CloudinaryService();
    }
    public async Task<bool> UploadImage(List<ProductSubColorFileAddFileDTO> pscfs)
    {
        
        foreach (var pscf in pscfs)
        {
            var name=await _cloudinaryService.UploadImageAsync(pscf.FileImage, "p-images");
            var result=_uow.ProductSubColorFile.Add(new ProductSubColorFile
            {
                ProductSubColorFile__Name = name,
                ProductSubColorFile__Index=pscf.ProductSubColorFile__Index,
                ProductSubColorFile__ProductSubColorId = pscf.ProductSubColorFile__ProductSubColorId
            });
        }
        return true;
    }
    public async Task<List<ProductSubColorFileGetFileDTO>> GetAllImage(int psc_Id)
    {
        var images = await _uow.ProductSubColorFile.GetAllAsync(x => x.ProductSubColorFile__ProductSubColorId == psc_Id);
        return images.Select(x => new ProductSubColorFileGetFileDTO
        {
            ProductSubColorFile__Id = x.ProductSubColorFile__Id,
            ProductSubColorFile__ProductColorId = x.ProductSubColorFile__ProductSubColorId,
            ProductSubColorFile__Name = x.ProductSubColorFile__Name,
            ProductSubColorFile__Index=x.ProductSubColorFile__Index
        }).OrderBy(x=>x.ProductSubColorFile__Index).ToList();
    }


    private string GetPublicId(string url)
    {
        string lastSegment = url.Substring(url.LastIndexOf('/') + 1);
        Console.WriteLine(value: Path.GetFileNameWithoutExtension(lastSegment)); // Kết quả: ddde4452-8183-49f7-9313-c7130206e781.jpg
        return "p-images/"+Path.GetFileNameWithoutExtension(lastSegment);
    }

    public void UpdateIndexImages(List<ProductSubColorFileUpdateFileDTO> pscfs)
    {
          foreach (var pscf in pscfs)
        {
            var file = _uow.ProductSubColorFile.Get(pscf.ProductSubColorFile__Id);
            file.ProductSubColorFile__Index = pscf.ProductSubColorFile__Index;
            if (file != null)
            {
                var result = _uow.ProductSubColorFile.Update(file);
            }
            
        }
    }
    public async Task RemoveImages(List<int> pscf_Ids)
    {
        foreach (var id in pscf_Ids)
        {
            var file = _uow.ProductSubColorFile.Get(id);
            if (file != null)
            {
                var result = _uow.ProductSubColorFile.Remove(file);
                if (result)
                {
                    var deleteIsSuccess = await _cloudinaryService.DeleteImageAsync(GetPublicId(file.ProductSubColorFile__Name));
                }
            }
        }
    }
}