using System.Net;
using System.Net.Mime;
using System.Text.Json;
using XblApp.API.Extensions;
using Domain.Exceptions;

namespace XblApp.API.Middlewares
{
    public class ExceptionHandlingMiddleware(
        RequestDelegate next,
        IWebHostEnvironment env)
    {
        private static readonly JsonSerializerOptions SerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                await WriteExceptionAsync(ex, HttpStatusCode.Unauthorized, context);
            }
            catch (NotFoundException ex)
            {
                await WriteExceptionAsync(ex, HttpStatusCode.NotFound, context);
            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(ex, HttpStatusCode.InternalServerError, context);
            }
        }

        private Task WriteExceptionAsync(Exception ex, HttpStatusCode status, HttpContext context)
        {
            var response = env.IsDevelopment() ?
                new ErrorResponse { Description = ex.Message, Message = ex.StackTrace } :
                new ErrorResponse { Description = "Внутренняя ошибка сервера. Keep calm", Message = string.Empty };

            return WriteResponseAsync(response, status, context);
        }

        private Task WriteResponseAsync(ErrorResponse response, HttpStatusCode status, HttpContext context)
        {
            context.Response.StatusCode = (int)status;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, SerializerOptions));
        }
    }


}
