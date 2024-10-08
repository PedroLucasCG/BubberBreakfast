namespace Bubberbreakfast.Contracts.Breakfast.CreateBreakfastRequest;

public record CreateBreakfastRequest (
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime,
    List<string> Savory,
    List<string> Sweet
);