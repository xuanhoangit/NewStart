using IVY.Application.DTOs;

namespace IVY.Application.Interfaces.IServices.Product;

public interface IOutfitService
{
    Task<bool> AddAsync(OutfitAddDTO addDTO);
}