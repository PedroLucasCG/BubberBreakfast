using Bubberbreakfast.Controllers;
using Microsoft.AspNetCore.Mvc;

public class ErrorsController : APIController {
    [Route("/error")]
    public IActionResult Error() {
        return Problem();
    }
}