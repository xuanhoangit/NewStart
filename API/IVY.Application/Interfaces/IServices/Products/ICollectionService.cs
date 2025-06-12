using IVY.Application.DTOs;

namespace IVY.Application.Interfaces.IServices.Product;
public interface ICollectionService {
     Task<List<CollectionGetDTO>> GetAllAsync();
}