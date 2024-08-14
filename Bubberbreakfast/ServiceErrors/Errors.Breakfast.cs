using ErrorOr;

namespace Bubberbreakfast.ServiceErrors;

public static class Errors {
    public static class Breakfast
    {
        public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must at least {Models.Breakfast.minNameLength} and have a max of {Models.Breakfast.maxNameLength}"
        );

        public static Error InvalidDescription => Error.Validation(
            code: "Breakfast.InvalidDescription",
            description: $"Breakfast description must at least {Models.Breakfast.minDescriptionLength} and have a max of {Models.Breakfast.maxDescriptionLength}"
        );

        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found...");
    }
}