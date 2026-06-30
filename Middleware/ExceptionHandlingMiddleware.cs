using System.Net;
using System.Text;
using System.Text.Json;

namespace Doctor.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = ex switch
        {
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            InvalidOperationException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new StandardApiResponse
        {
            Success = false,
            Code = statusCode,
            Message = ex.Message,
            Data = null,
            TraceId = context.TraceIdentifier,
            TimestampUtc = DateTime.UtcNow
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}







public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();

        var requestBody = "";

        if (context.Request.ContentLength > 0)
        {
            using var reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                leaveOpen: true);

            requestBody = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;
        }

        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();

        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);

        var responseText = await new StreamReader(context.Response.Body)
            .ReadToEndAsync();

        context.Response.Body.Seek(0, SeekOrigin.Begin);

        _logger.LogInformation(
            """
            HTTP REQUEST/RESPONSE

            Method: {Method}
            Path: {Path}
            StatusCode: {StatusCode}

            RequestBody:
            {RequestBody}

            ResponseBody:
            {ResponseBody}
            """,
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            requestBody,
            responseText);

        await responseBody.CopyToAsync(originalBodyStream);
    }
}