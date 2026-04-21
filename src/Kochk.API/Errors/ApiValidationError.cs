namespace Kochk.API.Errors;

public class ApiValidationError : ApiError
{
    public ApiValidationError()
        : base(400)
    {
        Errors = [];
    }

    public Dictionary<string, List<string>> Errors { get; set; }
}
