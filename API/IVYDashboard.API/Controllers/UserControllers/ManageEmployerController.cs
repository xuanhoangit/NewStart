using IVY.Application.DTOs;
using IVY.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.Controllers.UserControllers;
[Route("api/manager-user")]
public class ManageEmployerController : BaseController
{
    private readonly ManageEmployersService _mEmp;

    public ManageEmployerController(ManageEmployersService mEmp)
    {
        _mEmp = mEmp;
    }
    [HttpPost("lay-off")]
    public async Task<IActionResult> LayOffPersonnel(UserRole model)
    {
        try
        {
            var result = await _mEmp.LayOffPersonnel(model);
            return GetStatusReturn(result);
        }
        catch (System.Exception)
        {
            return StatusCode(500);
        }
    }
    [HttpPost("block")]
    public async Task<IActionResult> BlockUser(string userId)
    {
        try
        {
            var result = await _mEmp.BlockUser(userId);
            return GetStatusReturn(result);
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }
    [HttpPost("unblock")]
    public async Task<IActionResult> UnblockUser(string userId)
    {
        try
        {
            var result = await _mEmp.UnblockUser(userId);
            return GetStatusReturn(result);
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }
    [HttpPost("info")]
    public async Task<IActionResult> GetEmployerInfomation(EmailUser model)
    {
        try
        {
            var result = await _mEmp.GetEmployerInfomation(model);
            return GetStatusReturn(result);
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }
    [HttpPost("appointment")]
    public async Task<IActionResult> PositionAppointment(UserRole model)
    {
        try
        {
            var result = await _mEmp.PositionAppointment(model);
            return GetStatusReturn(result);
        }
        catch
        {

            return StatusCode(500);
        }
    }
}