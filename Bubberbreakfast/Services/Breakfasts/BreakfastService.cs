using Bubberbreakfast.Models;
using Bubberbreakfast.ServiceErrors;
using ErrorOr;

namespace Bubberbreakfast.Services.Breakfasts;
public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new Dictionary<Guid, Breakfast>();
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast, Guid id)
    {
        var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
        _breakfasts[id] = breakfast;

        return new UpsertedBreakfastResult(isNewlyCreated);
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if(_breakfasts.TryGetValue(id, out var breakfast)) {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }
}