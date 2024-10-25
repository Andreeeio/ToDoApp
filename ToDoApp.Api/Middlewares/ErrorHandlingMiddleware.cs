
using ToDoApp.Domain.Exceptions;

namespace ToDoApp.Api.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger; 
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch(InvalidOperationException e) 
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(e.Message);
        }
        catch (UnauthorizedExeption e)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync(e.Message);
        }
        catch (NotFoundException e) 
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(e.Message);
        }
         catch(Exception e)
        {
            context.Response.StatusCode = 500;
            Console.WriteLine(e);
        }
        catch 
        {
            context.Response.StatusCode = 500;
            _logger.LogWarning("Sth went wrong");   
        }
    }
}
