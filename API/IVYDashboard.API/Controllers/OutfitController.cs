using IVY.Application.DTOs;
using IVY.Application.Interfaces.IServices.Product;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers;
[Route("api/outfit")]
public class OutfitController : ControllerBase
{
    private readonly IOutfitService _outfit;

    public OutfitController(IOutfitService outfit)
    {
        _outfit = outfit;
    }
    [HttpPost("them-moi")]
    public async Task<IActionResult> AddAsync(OutfitAddDTO addDTO)
    {
        try
        {
            var result=await _outfit.AddAsync(addDTO);
            return result ? Ok() : StatusCode(500);
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }
}