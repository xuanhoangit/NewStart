using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Models.Products;

namespace IVY.Application.Services
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _uow;

        public ColorService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<List<ColorGetDTO>> GetAllColor()
        {
            var color=await _uow.Color.GetAllAsync();
            return color.Select(x=>new ColorGetDTO{
                Color__Id=x.Color__Id,
                Color__Image=x.Color__Image,
                Color__Name=x.Color__Name
            }).ToList();
        }
    }
}