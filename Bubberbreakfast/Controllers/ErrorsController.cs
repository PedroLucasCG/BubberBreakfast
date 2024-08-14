using Bubberbreakfast.Controllers;
using Microsoft.AspNetCore.Mvc;

public class ErrorsController : APIController {
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error() {
        return Problem();
    }
}