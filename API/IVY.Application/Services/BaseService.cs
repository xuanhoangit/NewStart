
using IVY.Application.Interfaces.IRepository;

namespace IVY.Application.IServices;
public abstract class BaseService
{
    private readonly IUnitOfWork _uow;
    public BaseService(IUnitOfWork uow)
    {
        _uow = uow;
    }
}