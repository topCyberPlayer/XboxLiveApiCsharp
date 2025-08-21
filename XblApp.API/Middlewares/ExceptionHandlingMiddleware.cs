using XblApp.Domain.Exceptions;

namespace XblApp.API.Middlewares
{
    public class ExceptionHandlingMiddleware(
        RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";

                // Подбор статуса по типу исключения
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    BusinessRuleException => StatusCodes.Status422UnprocessableEntity,
                    _ => StatusCodes.Status500InternalServerError
                };

                var response = env.IsDevelopment() ?
                    new { error = ex.Message, stackTrace = ex.StackTrace } :
                    new { error = "Внутренняя ошибка сервера. Keep calm", stackTrace = string.Empty };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }


}
