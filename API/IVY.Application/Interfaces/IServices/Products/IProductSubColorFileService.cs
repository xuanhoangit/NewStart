using IVY.Application.DTOs;

namespace IVY.Application.Interfaces.IServices.Product;
public interface IProductSubColorFileService {
    void UpdateIndexImages(List<ProductSubColorFileUpdateFileDTO> pscfs);
    Task<bool> UploadImage(List<ProductSubColorFileAddFileDTO> pscfs);
    Task<List<ProductSubColorFileGetFileDTO>> GetAllImage(int psc_Id);
    Task RemoveImages(List<int> pscf_Ids);
}