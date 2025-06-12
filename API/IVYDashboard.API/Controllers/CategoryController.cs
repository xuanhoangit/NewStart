using IVY.Application.Interfaces.IServices.Product;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers;
[Route("api/danh-muc")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _cs;

    public CategoryController(ICategoryService cs)
    {
        _cs = cs;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync (){
        var categories=await _cs.GetAllAsync();
        return Ok(categories);
    }
}