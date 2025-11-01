using System.Text.Json;
using FCG.Domain.Exceptions;

namespace FCG.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = StatusCodes.Status500InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = StatusCodes.Status400BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    error = "Falha na validação",
                    details = validationException.Errors
                });
                break;
            case UnauthorizedAccessException:
                code = StatusCodes.Status401Unauthorized;
                result = JsonSerializer.Serialize(new { error = "Não autorizado" });
                break;
            case KeyNotFoundException:
                code = StatusCodes.Status404NotFound;
                result = JsonSerializer.Serialize(new { error = "Recurso não encontrado" });
                break;
            case InvalidOperationException:
                code = StatusCodes.Status400BadRequest;
                result = JsonSerializer.Serialize(new { error = exception.Message });
                break;
            default:
                result = JsonSerializer.Serialize(new { error = "Ocorreu um erro interno no servidor" });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(result);
    }
}
