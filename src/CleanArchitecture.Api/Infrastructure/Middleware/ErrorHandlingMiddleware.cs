using System.Net;

using CleanArchitecture.Application.Common.Exceptions;

using FluentValidation;

using Newtonsoft.Json;

namespace CleanArchitecture.Api.Infrastructure.Middleware;

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}


public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
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

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode code;
        object content;

        switch (ex)
        {
            case EntityNotFoundException e:
                code = HttpStatusCode.NotFound;
                content = new { error = e.Message, entityName = e.EntityName, entityKey = e.EntityKey };
                break;

            case ValidationException e:
                code = HttpStatusCode.BadRequest;
                content = new { error = e.Message, validationErrors = e.Errors };
                break;

            default:
                code = HttpStatusCode.InternalServerError;
                content = new { error = ex.Message };
                break;
        }

        var result = JsonConvert.SerializeObject(content);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}

