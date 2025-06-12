using IVY.Application.Interfaces.IRepository;
using IVY.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers;
[Route("api/seeddata")]
public class SeedController : ControllerBase{
    private readonly IUnitOfWork uow;

    public SeedController(IUnitOfWork uow)
    {
        this.uow = uow;
    }
    [HttpPost]
    public IActionResult SeedData(){
        var seed=new Seed(uow);
        seed.Data();
        return Ok();
    }
}