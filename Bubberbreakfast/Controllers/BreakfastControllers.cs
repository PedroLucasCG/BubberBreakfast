using Microsoft.AspNetCore.Mvc;
using Bubberbreakfast.Contracts.Breakfast.CreateBreakfastRequest;
using Bubberbreakfast.Contracts.Breakfast.UpsertBreakfastRequest;
using Bubberbreakfast.Models;
using Bubberbreakfast.Contracts.Breakfast.BreakfastResponse;
using Bubberbreakfast.Services.Breakfasts;
using ErrorOr;

namespace Bubberbreakfast.Controllers.BreakfastControllers;

public class BreakfastController : APIController{
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

        var response = MapBreakfastResponse(breakfast);

        return CreatedAtAction(
            actionName: nameof(CreateBreakfast),
            routeValues: new {id = breakfast.Id},
            value: response
        );
    }

    [HttpGet]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfast = _breakfastService.GetBreakfast(id);

        return getBreakfast.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)
        );
    }

    private static Breakfast MapBreakfastResponse(Breakfast breakfast)
    {
        return new Breakfast(
                    breakfast.Id,
                    breakfast.Name,
                    breakfast.Description,
                    breakfast.StartDateTime,
                    breakfast.EndDateTime,
                    breakfast.LastModifiedDateTime,
                    breakfast.Savory,
                    breakfast.Sweet
                );
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

        var response = MapBreakfastResponse(breakfast);

        return CreatedAtAction(
            actionName: nameof(UpsertBreakfast),
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