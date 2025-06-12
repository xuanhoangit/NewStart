using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Enums;
using IVY.Domain.Models;

namespace IVY.Application.Services.Products;
public class CollectionService : ICollectionService
{
    private readonly IUnitOfWork _uow;

    public CollectionService(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<List<CollectionGetDTO>> GetAllAsync(){
        var collections=await _uow.Collection.GetAllAsync(x=>x.Collection__Status==(int)ProductStatus.Releasing);
        collections = collections.OrderByDescending(x => x.Collection__Id);
        var listCollectionDTO=new List<CollectionGetDTO>();
        foreach(var collection in collections){
            listCollectionDTO.Add(new CollectionGetDTO{
                Collection__Id=collection.Collection__Id,
                Collection__Name=collection.Collection__Name
            });
        }
        return listCollectionDTO;
    }
}