using Azure;
using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Models;
using IVY.Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers;
[Route("api/san-pham")]
public class ProductController : BaseController
{
    private readonly IProductService ps;
    private readonly string ServerError="Có lỗi vừa xảy ra!";

    public ProductController(IProductService ps)
    {
        this.ps = ps;
    }
    [HttpGet("{id}")]
    public IActionResult GetProductById(int id){
        return Ok();
    }
    [HttpGet("tim-kiem")]
    public IActionResult Search(string text){
        try
        {
            if(string.IsNullOrWhiteSpace(text)){
                return Ok(null);
            }

            return Ok(ps.Search(text));
        }
        catch (System.Exception e)
        {
            return StatusCode(500,ServerError+" "+e.Message);
        }
    }
    [HttpPatch("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id không hợp lệ");
            }
            return ps.Remove(id) ? Ok():StatusCode(500);
            
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }
    //PassExellence
    [HttpPost("them-moi")]
    public async Task<IActionResult> Add([FromBody]ProductFormAddDTO productFormAddDTO){
        try
        {

            var result=await ps.Add(productFormAddDTO);


            if (result.Status == ResultStatus.Created) {
                return CreatedAtAction(nameof(GetProductById), new { id = result.Data.Product__Id }, result.Data);
            }
            return GetStatusReturn(result);
         }
        catch (System.Exception e)
        {
            return StatusCode(500,ServerError+" "+e.Message);
        }
    }
    [HttpGet("loc")]
    public async Task<IActionResult> ProductFilter(ProductFilter filter){
        // var a=await ps.GetProductByFilter(filter);
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(await ps.GetProductByFilter(filter));
        }
        catch (System.Exception e)
        {
            
            return StatusCode(500,ServerError+" "+e.Message);
        }
    }
    [HttpGet("page/{page}")]
    public async Task<IActionResult> ProductFilter(int page){
        // var a=await ps.GetProductByFilter(filter);
        try
        {
            return Ok(await ps.GetProducts(page));
        }
        catch (System.Exception e)
        {
            
            return StatusCode(500,ServerError+" "+e.Message);
        }
    }
}