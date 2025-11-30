using Newtonsoft.Json;
using Service.DTOs.CommonDtos;
using Service.Exceptions;
using System.Net;

namespace Final.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            string errorName = "Ooops,Error!";
            string errorMessage = "An unexpected error occurred.Contact the server";

            if (exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                errorMessage = "You are not allowed to access this section.";
            }
            else if (exception is CustomException e)
            {
                statusCode = e.StatusCode;
                errorMessage = e.Message;
            }

            context.Response.ContentType = "application/json";

            var errorDto = new ErrorVM
            {
                StatusCode = (int)statusCode,
                Message = errorMessage,
                Name = errorName
            };

            var result = JsonConvert.SerializeObject(errorDto);

            if (context.Request.Headers["Accept"].ToString().Contains("application/json"))
            {
                await context.Response.WriteAsync(result);
            }
            else
            {
                var errorPath = "/Home/Error";
                var query = $"?json={Uri.EscapeDataString(result)}";
                var fullPath = $"{errorPath}{query}";

                context.Response.Redirect(fullPath);
                await Task.CompletedTask;
            }
        }
    }
}
