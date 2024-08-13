using Bubberbreakfast.Models;
using ErrorOr;

namespace Bubberbreakfast.Services.Breakfasts;

public interface IBreakfastService {
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    ErrorOr<Breakfast> GetBreakfast(Guid id);
    ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast, Guid id);
}