using Microsoft.AspNetCore.Http.HttpResults;

namespace IVY.Application.DTOs;
public enum ResultStatus
{
    Success = 0,         // 200 OK
    Conflict = 1,        // 409 Conflict
    BadRequest = 2,      // 400 Bad Request
    NotFound = 3,        // 404 Not Found
    Created = 4,         // 201 Created
    NoContent = 5,       // 204 No Content
    Unauthorized = 6,    // 401 Unauthorized
    Forbidden = 7,       // 403 Forbidden
    InternalError = 8    // 500 Internal Server Error
}

public class Result<T>
{
    public bool IsSuccess => Status == ResultStatus.Success || Status == ResultStatus.Created;
    public T? Data { get; set; }
    // public string? Message { get; set; }
    public ResultStatus Status { get; set; }

    public static Result<T> Success(T data) => new() { Data = data, Status = ResultStatus.Success};
    public static Result<T> Created(T data) => new() { Data = data, Status = ResultStatus.Created };
    public static Result<T> Failure(ResultStatus status) => new() {  Status = status };
}


