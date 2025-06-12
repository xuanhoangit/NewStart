using IVY.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

public abstract class BaseController<T> : ControllerBase
{
    protected IActionResult GetStatusReturn(Result<T> result)
    {
         return result.Status switch
        {
            ResultStatus.Success => Ok(new {result.Data}),
            ResultStatus.BadRequest => BadRequest(),
            ResultStatus.Conflict => Conflict(),
            ResultStatus.NotFound => NotFound(),
            _ => StatusCode(500, "Server error")
        };
    }
}