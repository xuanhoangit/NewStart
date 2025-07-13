using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.Controllers.UserControllers;
[Route("api/employee")]
public class ManageEmployerController : BaseController
{
    private readonly IEmployeeManagerService _mEmp;

    public ManageEmployerController(IEmployeeManagerService mEmp)
    {
        _mEmp = mEmp;
    }
    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateEmployeeInfomation([FromForm] RegisterDto model)
    {
        var result = await _mEmp.UpdateProfile(model);
        return GetStatusReturn(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetEmployees(EmployeeFilter filter)
    {
        var result=await _mEmp.GetEmployees(filter);
        return GetStatusReturn(result);
    }
    [HttpGet("no-access")]
    public async Task<IActionResult> GetEmployeesNoAccess()
    {
        var result=await _mEmp.GetEmployeeNoRole();
        return GetStatusReturn(result);
    }
    [HttpPost("revoke-access")]
    public async Task<IActionResult> RevokeAccess([FromBody]UserRole model)
    {
        try
        {
            var result = await _mEmp.RevokeAccess(model);
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