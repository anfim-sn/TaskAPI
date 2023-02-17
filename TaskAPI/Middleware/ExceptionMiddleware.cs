using System.Net;

namespace TaskAPI.Api.Middleware;

public class ExceptionMiddleware
{
  private readonly ILogger<ExceptionMiddleware> _logger;
  private readonly RequestDelegate next;

  public ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger
  )
  {
    this.next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        $"Something went wrong wile processing {context.Request.Path}"
      );
      _logger.LogError($"{e}");
      await HandleExceptionAsync(context, e);
    }
  }

  private Task HandleExceptionAsync(HttpContext context, Exception e)
  {
    context.Response.ContentType = "application/json";
    var statusCode = HttpStatusCode.InternalServerError;

    context.Response.StatusCode = (int)statusCode;

    return context.Response.WriteAsync(e.Message);
  }
}
