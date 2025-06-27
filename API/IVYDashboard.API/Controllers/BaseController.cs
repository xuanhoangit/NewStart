using IVY.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : ControllerBase
{
    protected IActionResult GetStatusReturn<T>(Result<T> result)
    {
         return result.Status switch
        {
            ResultStatus.Success => Ok(result.Data),
            ResultStatus.BadRequest => BadRequest(),
            ResultStatus.Conflict => Conflict(),
            ResultStatus.NotFound => NotFound(),
            ResultStatus.Unauthorized => Unauthorized(),
            _ => StatusCode(500, "Server error")
        };
    }
}