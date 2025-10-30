using System.Net;
using System.Text.Json;
using ApiPrueba.src.Domain.Exeptions;
using Microsoft.AspNetCore.Http;


namespace ApiPrueba.src.Infrastructure.Middleware;



    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Ocurrió una excepción: {Message}", ex.Message);

            context.Response.ContentType = "application/json";
           
            if (ex is NotFoundException)
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        else if (ex is ArgumentException)
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        else if (ex is UnauthorizedAccessException)
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        else
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


        var response = new
            {
                statusCode = context.Response.StatusCode,
                error = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }


