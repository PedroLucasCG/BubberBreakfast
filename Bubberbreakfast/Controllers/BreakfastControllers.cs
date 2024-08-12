using Microsoft.AspNetCore.Mvc;
using Bubberbreakfast.Contracts.Breakfast.CreateBreakfastRequest;
using Bubberbreakfast.Contracts.Breakfast.UpsertBreakfastRequest;
using Bubberbreakfast.Models;
using Bubberbreakfast.Contracts.Breakfast.BreakfastResponse;
using System.Security.Cryptography;
using Bubberbreakfast.Services.Breakfasts;

namespace Bubberbreakfast.Controllers.BreakfastControllers;

[ApiController]
[Route("[controller]")]
public class BreakfastController : ControllerBase{
    private readonly IBreakfastService _breakfastService;
    public BreakfastController(IBreakfastService breakfastService) {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request) {
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        _breakfastService.CreateBreakfast(breakfast);

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return CreatedAtAction(
            actionName: nameof(CreateBreakfast),
            routeValues: new {id = breakfast.Id},
            value: response
        );
    }

    [HttpGet]
    public IActionResult GetBreakfast(Guid id) {
        Breakfast breakfast = _breakfastService.GetBreakfast(id);
        var reponse = new Breakfast(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
        return Ok(reponse);
    }

    [HttpPut]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request) {
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
        _breakfastService.UpsertBreakfast(breakfast, id);

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return CreatedAtAction(
            actionName: nameof(CreateBreakfast),
            routeValues: new {id = breakfast.Id},
            value: response
        );
    }

    [HttpDelete]
    public IActionResult DeleteBreakfast(Guid id) {
        _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }
}