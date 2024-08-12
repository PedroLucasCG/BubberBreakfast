using Bubberbreakfast.Models;

namespace Bubberbreakfast.Services.Breakfasts;

public interface IBreakfastService {
    void CreateBreakfast(Breakfast breakfast);
    void DeleteBreakfast(Guid id);
    Breakfast GetBreakfast(Guid id);
    void UpsertBreakfast(Breakfast breakfast, Guid id);
}