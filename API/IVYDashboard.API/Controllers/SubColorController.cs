using IVY.Application.DTOs;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers;
[Route("api/mau")]
public class SubColorController : BaseController<SubColor>
{
    private readonly ISubColorService _scs;
    private readonly string ServerError="Có lỗi vừa xảy ra!";
    public SubColorController(ISubColorService scs)
    {
        _scs = scs;
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id) {
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        try
        {
            return Ok(await _scs.GetAllAsync());
        }
        catch (System.Exception e)
        {
            
            return StatusCode(500, ServerError + " " + e.Message);
        }
    }
    [HttpPost("them-moi")]
    public async Task<IActionResult> Add([FromForm] SubColorAddDTO model){
        try
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var subColor = await _scs.AddSubColor(model);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return subColor.Status == ResultStatus.Created ? CreatedAtAction(nameof(Get), new { id = subColor.Data.SubColor__Id }, subColor.Data) :
             GetStatusReturn(subColor);
       }
        catch (System.Exception e)
        {
            return StatusCode(500, ServerError + " " + e.Message);
        }
    }
}