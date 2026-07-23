using Microsoft.AspNetCore.Mvc;
using Offices.Application.Results;
using Offices.Domain.Models;

namespace Offices.Presentation.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return HandleFailure(result.Error);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
        {
            return Ok();
        }

        return HandleFailure(result.Error);
    }

    protected IActionResult HandleCreationResult<T>(Result<T> result, string actionName, object location)
    {
        if(result.IsSuccess)
            return CreatedAtAction(actionName, location, result.Value);

        return HandleFailure(result.Error);
    }

    protected IActionResult HandleFailure(Error error)
    {
        var apiResponse = ApiResponse.Failure(error.Message);

        return error.Type switch
        {
            ErrorType.NotFound => NotFound(apiResponse),
            ErrorType.Conflict => Conflict(apiResponse),
            ErrorType.Forbidden => StatusCode(403, apiResponse),
            ErrorType.Validation => BadRequest(apiResponse),
            _ => BadRequest(apiResponse)
        };
    }

    protected IActionResult HandleValidationFailure(IDictionary<string, string[]> validationErrors)
    {
        var errors = validationErrors.SelectMany(kv => kv.Value).ToList();

        var apiResponse = ApiResponse.Failure(errors, "Ошибка валидации данных");
        return BadRequest(apiResponse);
    }
}