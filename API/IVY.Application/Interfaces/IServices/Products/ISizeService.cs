using IVY.Application.DTOs;

namespace IVY.Application.Interfaces.IServices.Product
{
    public interface ISizeService
    {
        bool UpdateSizeQuantity(SizeDTO sizeDTO);
    }
}