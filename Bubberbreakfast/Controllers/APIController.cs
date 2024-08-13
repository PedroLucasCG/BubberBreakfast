using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Bubberbreakfast.Controllers;

[ApiController]
[Route("[controller]")]
public class APIController : ControllerBase
{
    public IActionResult Problem(List<Error> errors) {
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}