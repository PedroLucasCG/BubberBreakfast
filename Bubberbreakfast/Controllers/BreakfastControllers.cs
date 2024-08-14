using Microsoft.AspNetCore.Mvc;
using Bubberbreakfast.Contracts.Breakfast.CreateBreakfastRequest;
using Bubberbreakfast.Contracts.Breakfast.UpsertBreakfastRequest;
using Bubberbreakfast.Models;
using Bubberbreakfast.Services.Breakfasts;
using ErrorOr;
using Bubberbreakfast.Contracts.Breakfast.BreakfastResponse;

namespace Bubberbreakfast.Controllers.BreakfastControllers;

public class BreakfastController : APIController{
    private readonly IBreakfastService _breakfastService;
    public BreakfastController(IBreakfastService breakfastService) {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var requestToBreakfastResult = Breakfast.From(request);

        if (requestToBreakfastResult.IsError) return Problem(requestToBreakfastResult.Errors);
        var breakfast = requestToBreakfastResult.Value;

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
        ErrorOr<Breakfast> requestBreakfastResult = Breakfast.From(id, request);

        if (requestBreakfastResult.IsError) return Problem(requestBreakfastResult.Errors);
        var breakfast = requestBreakfastResult.Value;

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

    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
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

    private CreatedAtActionResult CreatedAtGetBreakfast(Breakfast breakfast, BreakfastResponse response)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: response
        );
    }
}