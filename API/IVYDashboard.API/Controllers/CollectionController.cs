using IVY.Application.Interfaces.IServices.Product;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers;
[Route("api/bo-suu-tap")]
public class CollectionController : ControllerBase
{
    private readonly ICollectionService _cs;

    public CollectionController(ICollectionService cs)
    {
        _cs = cs;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync (){
        var collections=await _cs.GetAllAsync();
        return Ok(collections);
    }
}