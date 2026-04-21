namespace Kochk.API.Errors;

public class ApiError
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public ApiError(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetMessage(StatusCode);
    }

    private static string? GetMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            402 => "Payment Required",
            403 => "Forbidden",
            404 => "Not Found",
            _ => null,
        };
    }
}
