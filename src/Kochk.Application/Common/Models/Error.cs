namespace Kochk.Application.Common.Models;

public record Error(
    string Id,
    ErrorType Type,
    string Description,
    Dictionary<string, List<string>>? ValidationErrors = null
)
{
    public static readonly Error None = new(string.Empty, ErrorType.None, string.Empty);

    public static Error Validation(
        string code,
        string description,
        Dictionary<string, List<string>> validationErrors
    ) => new(code, ErrorType.Validation, description, validationErrors);
}

public enum ErrorType
{
    None = 0,
    NotFound = 1,
    Validation = 2,
    Unauthorized = 3,
}
