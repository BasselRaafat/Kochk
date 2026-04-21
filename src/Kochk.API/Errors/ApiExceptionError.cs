namespace Kochk.API.Errors;

public class ApiExceptionError : ApiError
{
    public string Details { get; }

    public ApiExceptionError(int statusCode, string? message = null, string? details = null)
        : base(statusCode, message)
    {
        Details = details!;
    }
}
