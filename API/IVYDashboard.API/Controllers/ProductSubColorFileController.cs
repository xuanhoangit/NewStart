using IVY.Application.DTOs;
using IVY.Application.Interfaces.IServices.Product;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers
{
    [Route("api/hinh-anh-san-pham")]
    public class ProductSubColorFileController : ControllerBase
    {
        private readonly IProductSubColorFileService _pscf;

        public ProductSubColorFileController(IProductSubColorFileService pscf)
        {
            _pscf = pscf;
        }
        [HttpGet("{psc_Id}")]
        public async Task<IActionResult> GetAll(int psc_Id)
        {
            return Ok(await _pscf.GetAllImage(psc_Id));
        }
        [HttpPost("cap-nhat")]
        public async Task<IActionResult> Update([FromForm]RequestUpdateFileDTO request)
        {
            try
            {
                if (request.RemoveIds != null)
                {
                    await _pscf.RemoveImages(request.RemoveIds);
                }
                if (request.FileUpdates != null)
                {
                    _pscf.UpdateIndexImages(request.FileUpdates);
                }
                if (request.FileAdds != null)
                {
                    await _pscf.UploadImage(request.FileAdds);
                }
                return Ok(new {message="Cập nhật hình ảnh thành công!",files=await _pscf.GetAllImage(request.Psc_Id)});
            }
            catch (System.Exception)
            {

                return StatusCode(500, "Lỗi server");
            }
        }

    }
}