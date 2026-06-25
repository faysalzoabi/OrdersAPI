namespace OrderApi.Middleware
{
  public class RequestLoggingMiddleware
  {
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate requestDelegate)
    {
      _next = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      // logic - request
      System.Console.WriteLine("this is before request");
      await _next(context);
      System.Console.WriteLine("this is after response");
    }
  }
}