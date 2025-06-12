using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;

namespace IVY.Application.Services.Products
{
    public class SizeService:ISizeService
    {
        private readonly IUnitOfWork _uow;

        public SizeService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public bool UpdateSizeQuantity(SizeDTO sizeDTO)
        {
            var size = _uow.Size.Get(sizeDTO.Size__Id);
            size.Size__L = sizeDTO.Size__L;
            size.Size__S = sizeDTO.Size__S;
            size.Size__M = sizeDTO.Size__M;
            size.Size__XXl = sizeDTO.Size__XXl;
            size.Size__XL = sizeDTO.Size__XL;
            return _uow.Size.Update(size);
        }
    }
}