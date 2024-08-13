using Microsoft.AspNetCore.Mvc;
using Bubberbreakfast.Contracts.Breakfast.CreateBreakfastRequest;
using Bubberbreakfast.Contracts.Breakfast.UpsertBreakfastRequest;
using Bubberbreakfast.Models;
using Bubberbreakfast.Services.Breakfasts;
using ErrorOr;

namespace Bubberbreakfast.Controllers.BreakfastControllers;

public class BreakfastController : APIController{
    private readonly IBreakfastService _breakfastService;
    public BreakfastController(IBreakfastService breakfastService) {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
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

        ErrorOr<Created> createBreakfastResponse = _breakfastService.CreateBreakfast(breakfast);

        var response = MapBreakfastResponse(breakfast);

        return createBreakfastResponse.Match(
            created => CreatedAtGetBreakfast(breakfast, response),
            errors => Problem(createBreakfastResponse.Errors)
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

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request) {
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );
        ErrorOr<UpsertedBreakfastResult> updateBreakfast = _breakfastService.UpsertBreakfast(breakfast, id);
        var response = MapBreakfastResponse(breakfast);
        
        return updateBreakfast.Match(
            updated => updated.isNewlyCreated ? CreatedAtGetBreakfast(breakfast, response) : NoContent(),
            errors => Problem(updateBreakfast.Errors)
        );
    }

    [HttpDelete]
    public IActionResult DeleteBreakfast(Guid id) {
        
        ErrorOr<Deleted> deleteBreakfast = _breakfastService.DeleteBreakfast(id);
        
        return deleteBreakfast.Match(
            deleted => NoContent(),
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

    private CreatedAtActionResult CreatedAtGetBreakfast(Breakfast breakfast, Breakfast response)
    {
        return CreatedAtAction(
                    actionName: nameof(GetBreakfast),
                    routeValues: new { id = breakfast.Id },
                    value: response
                );
    }
}