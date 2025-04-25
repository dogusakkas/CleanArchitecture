using Domain.Entities;
using FluentValidation;
using Persistance.Context;

namespace CleanArchitecture.WebApi.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly AppDbContext _context;

        public ExceptionMiddleware(AppDbContext context)
        {
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionToDatabaseAsync(ex, context.Request);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            if (ex is ValidationException validationException)
            {
                context.Response.StatusCode = 400;

                var validationErrors = validationException.Errors
                    .Select(x => x.PropertyName)
                    .ToList();

                var errorDetails = new ValidationErrorDetails
                {
                    Errors = validationErrors,
                    StatusCode = context.Response.StatusCode
                };

                return context.Response.WriteAsync(errorDetails.ToString());
            }

            var generalError = new ErrorResult
            {
                Message = ex.Message,
                StatusCode = context.Response.StatusCode
            };

            return context.Response.WriteAsync(generalError.ToString());
        }

        private async Task LogExceptionToDatabaseAsync(Exception ex, HttpRequest request)
        {
            ErrorLog errorLog = new()
            {
                ErrorMessage = ex.Message,
                RequestMethod = request.Method,
                RequestPath = request.Path,
                TimeStamp = DateTime.Now,
                StackTrace = ex.StackTrace
            };

            await _context.Set<ErrorLog>().AddAsync(errorLog, default);
            await _context.SaveChangesAsync(default);
        }
    }
}
