using Bubberbreakfast.Contracts.Breakfast.BreakfastResponse;
using Bubberbreakfast.Models;
using Bubberbreakfast.ServiceErrors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bubberbreakfast.Services.Breakfasts;
public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new Dictionary<Guid, Breakfast>();
    public void CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
    }

    public void UpsertBreakfast(Breakfast breakfast, Guid id)
    {
        _breakfasts[id] = breakfast;  
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if(_breakfasts.TryGetValue(id, out var breakfast)) {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }
}