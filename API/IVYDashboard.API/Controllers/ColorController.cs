using IVY.Application.Interfaces.IServices.Product;
using Microsoft.AspNetCore.Mvc;


namespace IVYDashboard.API.Controllers
{   
    [Route("api/nhom-mau")]
    public class ColorController:ControllerBase
    {
        private readonly IColorService _cs;
        private readonly string ServerError="Có lỗi vừa xảy ra!";
        public ColorController(IColorService cs)
        {
            _cs = cs;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            try
            {
                return Ok(await _cs.GetAllColor());
            }
            catch (System.Exception e)
            {
                
                return StatusCode(500,ServerError+" "+e.Message);
            }
        }
    }
}