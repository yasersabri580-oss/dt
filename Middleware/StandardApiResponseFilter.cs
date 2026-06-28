using System.Reflection;
using System.Linq;                     // required for .OfType<> and .Any()
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Accounting_helal.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class SkipResponseEnvelopeAttribute : Attribute
{
}

public class StandardApiResponseFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // Allow actions decorated with [SkipResponseEnvelope] to pass through unmodified.
        if (context.ActionDescriptor.EndpointMetadata.OfType<SkipResponseEnvelopeAttribute>().Any())
        {
            await next();
            return;
        }

        var result = context.Result;
        switch (result)
        {
            case ObjectResult objectResult:
                context.Result = WrapObjectResult(context, objectResult);
                break;

            // ---- JsonResult is now wrapped correctly -------------------------
            case JsonResult jsonResult:
                // Convert JsonResult to an ObjectResult. No need to copy ContentTypes
                // because ObjectResult already defaults to JSON.
                var jsonObjectResult = new ObjectResult(jsonResult.Value)
                {
                    StatusCode = jsonResult.StatusCode
                };
                context.Result = WrapObjectResult(context, jsonObjectResult);
                break;
            // ------------------------------------------------------------------

            case EmptyResult:
                context.Result = BuildEnvelopeResult(context, StatusCodes.Status200OK,
                    "Operation completed successfully.", null);
                break;

            case StatusCodeResult statusCodeResult when statusCodeResult.StatusCode != StatusCodes.Status204NoContent:
                context.Result = BuildEnvelopeResult(
                    context,
                    statusCodeResult.StatusCode,
                    GetDefaultMessage(statusCodeResult.StatusCode),
                    null);
                break;
        }

        await next();
    }

    private static ObjectResult WrapObjectResult(ResultExecutingContext context, ObjectResult objectResult)
    {
        var statusCode = objectResult.StatusCode ?? context.HttpContext.Response.StatusCode;
        if (statusCode == 0)
        {
            statusCode = StatusCodes.Status200OK;
        }

        if (objectResult.Value is StandardApiResponse)
        {
            objectResult.StatusCode = statusCode;
            return objectResult;
        }

        var (message, data, forcedSuccess) = ExtractPayload(objectResult.Value, statusCode);
        var success = forcedSuccess ?? (statusCode >= 200 && statusCode < 300);

        var envelope = new StandardApiResponse
        {
            Success = success,
            Code = statusCode,
            Message = message,
            Data = data,
            TraceId = context.HttpContext.TraceIdentifier,
            TimestampUtc = DateTime.UtcNow
        };

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode,
            DeclaredType = typeof(StandardApiResponse)
        };
    }

    private static ObjectResult BuildEnvelopeResult(
        ResultExecutingContext context,
        int statusCode,
        string message,
        object? data)
    {
        var envelope = new StandardApiResponse
        {
            Success = statusCode >= 200 && statusCode < 300,
            Code = statusCode,
            Message = message,
            Data = data,
            TraceId = context.HttpContext.TraceIdentifier,
            TimestampUtc = DateTime.UtcNow
        };

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode,
            DeclaredType = typeof(StandardApiResponse)
        };
    }

    private static (string message, object? data, bool? success) ExtractPayload(object? value, int statusCode)
    {
        if (value == null)
        {
            return (GetDefaultMessage(statusCode), null, null);
        }

        if (value is ValidationProblemDetails validation)
        {
            var details = validation.Errors
                .SelectMany(kvp => kvp.Value.Select(error => new { field = kvp.Key, error }))
                .ToList();
            return ("Validation failed.", details, false);
        }

        var valueType = value.GetType();
        // Ensure the namespace containing ApiResponse<> is imported, e.g.:
        // using Accounting_helal.Models;
      

        if (value is ProblemDetails pd)
        {
            return (pd.Title ?? pd.Detail ?? GetDefaultMessage(statusCode), pd.Extensions, false);
        }

        var explicitMessage = ExtractMessageProperty(value);
        if (!string.IsNullOrWhiteSpace(explicitMessage))
        {
            return (explicitMessage!, value, statusCode >= 200 && statusCode < 300);
        }

        return (GetDefaultMessage(statusCode), value, null);
    }

    private static string? ExtractMessageProperty(object value)
    {
        // Single case‑insensitive lookup is sufficient.
        var prop = value.GetType().GetProperty("message",
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        return prop?.GetValue(value)?.ToString();
    }

    private static string GetDefaultMessage(int statusCode) => statusCode switch
    {
        StatusCodes.Status200OK => "Operation completed successfully.",
        StatusCodes.Status201Created => "Resource created successfully.",
        StatusCodes.Status202Accepted => "Request accepted successfully.",
        StatusCodes.Status400BadRequest => "The request is invalid.",
        StatusCodes.Status401Unauthorized => "Authentication is required.",
        StatusCodes.Status403Forbidden => "You do not have permission to perform this action.",
        StatusCodes.Status404NotFound => "The requested resource was not found.",
        StatusCodes.Status409Conflict => "The request could not be completed due to a conflict.",
        StatusCodes.Status422UnprocessableEntity => "The request could not be processed.",
        StatusCodes.Status500InternalServerError => "An unexpected server error occurred.",
        _ => "Request processed."
    };
}

public class StandardApiResponse
{
    public bool Success { get; set; }
    public int Code { get; set; }
    public string Message { get; set; } = null!;
    public object? Data { get; set; }
    public string TraceId { get; set; } = null!;
    public DateTime TimestampUtc { get; set; }
}