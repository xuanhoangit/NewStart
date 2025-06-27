using IVY.Application.DTOs;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers;

[Route("api/mau-san-pham")]
public class ProductSubColorController : BaseController
{
    private readonly IProductSubColorService _psc;
        private readonly string ServerError="Có lỗi vừa xảy ra!";

    public ProductSubColorController(IProductSubColorService psc)
    {
        _psc = psc;
    }
    [HttpGet("{id}")]
    public IActionResult GetProductSubColorById(int id)
    {
        var data=_psc.GetDTO(id);
        return Ok(data);
    }
    [HttpPut("cap-nhat")]
    public IActionResult Update([FromBody]ProductSubColorGetDTO dto)
    {
        var productSubColor = _psc.UpdateProductSubColor(dto);
        return GetStatusReturn(productSubColor);
    }
    [HttpPost("them-moi")]
    public async Task<IActionResult> Add([FromBody] ProductSubColorFormAddDTO pscDTO)
    {
        try
        {
            Result<ProductSubColorGetHomeShowDTO>? productSubColor = await _psc.AddProductSubColor(pscDTO);
            if (productSubColor.Status == ResultStatus.Created)
            {
                return CreatedAtAction(nameof(GetProductSubColorById), new { id = productSubColor.Data.ProductSubColor__Id }, productSubColor.Data);
            }
            return GetStatusReturn(productSubColor);

        }
        catch (System.Exception e)
        {

            return StatusCode(500, ServerError + " " + e.Message);
        }
    }
    [HttpPatch("{id}")]
    public IActionResult Remove(int id)
    {
       try
       {
         if (id <= 0)
        {
            return BadRequest("Id không hợp lệ");
        }
            var result = _psc.Remove(id);
            return result ? Ok() : StatusCode(500, "Có lỗi xảy ra!");
       }
       catch (System.Exception e)
       {
        
            return StatusCode(500, ServerError +" "+e.Message);
       }
    }
}