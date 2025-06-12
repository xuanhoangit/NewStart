using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Models.Products;

namespace IVY.Application.Services
{
    public class OutfitService : IOutfitService
    {
        private readonly IUnitOfWork _uow;

        public OutfitService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<bool> AddAsync(OutfitAddDTO addDTO)
        {
            var outfit = new Outfit
            {
                Outfit__Key = addDTO.Outfit__Key,
                Outfit__ProductSubColorId = addDTO.Outfit__ProductSubColorId
            };
            return await _uow.Outfit.AddAsync(outfit);
        }
    }
}