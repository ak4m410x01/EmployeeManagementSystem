using EmployeeManagementSystem.Shared.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using DataAnnotationsValidationException = System.ComponentModel.DataAnnotations.ValidationException;
using FluentValidationException = FluentValidation.ValidationException;

namespace EmployeeManagementSystem.Presentation.Middlewares.Exceptions
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly ResponseHandler _responseHandler;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, ResponseHandler responseHandler)
        {
            _next = next;
            _logger = logger;
            _responseHandler = responseHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (IsPostOrPutRequest(context) && !IsValidContentType(context))
            {
                await HandleInvalidContentTypeAsync(context);
                return;
            }

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private bool IsPostOrPutRequest(HttpContext context) =>
            context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put;

        private bool IsValidContentType(HttpContext context) =>
            context.Request.Headers.TryGetValue("Content-Type", out var contentType) &&
            IsValidContentType(contentType!);

        private async Task HandleInvalidContentTypeAsync(HttpContext context)
        {
            var responseModel = _responseHandler.MethodNotAllowed405<object>("Unsupported Media Type");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseModel.StatusCode;
            await context.Response.WriteAsJsonAsync(responseModel);

            _logger.LogWarning("Invalid content type: {ContentType} for request path: {Path}",
                context.Request.ContentType, context.Request.Path);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var responseModel = new Response<object>
            {
                Succeeded = false,
            };

            switch (exception)
            {
                case UnauthorizedAccessException ex:
                    responseModel = _responseHandler.Unauthorized401<object>(ex.Message);
                    LogException(context, ex, HttpStatusCode.Unauthorized, "Unauthorized Access");
                    break;

                case DataAnnotationsValidationException ex:
                    responseModel = _responseHandler.BadRequest400<object>("Validation failed.");
                    responseModel.AddErrors(ex.Message.Split(';'));
                    LogException(context, ex, HttpStatusCode.BadRequest, "Validation Error (DataAnnotations)");
                    break;

                case FluentValidationException ex:
                    responseModel = _responseHandler.BadRequest400<object>("Validation failed.");
                    responseModel.AddErrors(ex.Message.Split(';'));
                    LogException(context, ex, HttpStatusCode.BadRequest, "Validation Error (FluentValidation)");
                    break;

                case KeyNotFoundException ex:
                    responseModel = _responseHandler.NotFound404<object>("Resource not found.");
                    responseModel.AddError(ex.Message);
                    LogException(context, ex, HttpStatusCode.NotFound, "Resource Not Found");
                    break;

                case DbUpdateException ex:
                    responseModel = _responseHandler.BadRequest400<object>("Database update error.");
                    responseModel.AddError(ex.Message);
                    LogException(context, ex, HttpStatusCode.BadRequest, "Database Update Error");
                    break;

                default:
                    // Handle general or unexpected exceptions
                    if (exception.InnerException != null)
                    {
                        responseModel = _responseHandler.InternalServerError500<object>("An unexpected error occurred.");
                        responseModel.AddError(exception.InnerException.Message);
                    }

                    LogException(context, exception, HttpStatusCode.InternalServerError, "Unexpected Error");

                    // Handle specific validation errors, e.g., Invalid CategoryId
                    if (context.Features.Get<IExceptionHandlerFeature>()?.Error is InvalidOperationException &&
                        context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
                    {
                        responseModel = _responseHandler.BadRequest400<object>("One or more validation errors occurred.");
                    }

                    responseModel.AddError(exception.Message);

                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseModel.StatusCode;
            await context.Response.WriteAsJsonAsync(responseModel);

            _logger.LogError(exception, exception.Message);
        }

        private static bool IsValidContentType(string contentType) =>
            contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase) ||
            contentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) ||
            contentType.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase);

        // Log exception with request context and severity based on status code
        private void LogException(HttpContext context, Exception exception, HttpStatusCode statusCode, string message)
        {
            // Log with relevant details including request path and method
            _logger.LogError(exception,
                "{Message}: {Path} {Method} returned {StatusCode}. Exception: {ExceptionMessage}",
                message, context.Request.Path, context.Request.Method, statusCode, exception.Message);

            // Optionally, you can log more details such as headers, user claims, etc.
        }
    }
}