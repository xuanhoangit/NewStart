
using IVY.Application.DTOs;
using IVY.Application.Interfaces.IServices.Product;
using Microsoft.AspNetCore.Mvc;
[Route("api/size")]
public class SizeController : ControllerBase
{
    private readonly ISizeService _s;

    public SizeController(ISizeService s)
    {
        _s = s;
    }
    [HttpPut("cap-nhat")]
    public IActionResult Update([FromBody]SizeDTO sizeDTO)
    {
       try
       {
            return Ok(_s.UpdateSizeQuantity(sizeDTO));
       }
       catch (System.Exception)
       {

            return StatusCode(500, "Lỗi server");
       }
    }
}