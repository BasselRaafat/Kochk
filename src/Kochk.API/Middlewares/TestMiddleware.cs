
namespace Kochk.API.Middlewares;

public class TestMiddleware : IMiddleware
{
    public TestMiddleware()
    {
    }
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}
