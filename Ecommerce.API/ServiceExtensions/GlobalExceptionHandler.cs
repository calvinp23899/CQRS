using Ecommerce.Business.Models;
using Ecommerce.Core.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.API.ServiceExtensions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new Response<object>();
            var endpoint = httpContext.Request.Path;

            if (exception is ValidationException fluentException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                foreach (var error in fluentException.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }

                response.ErrorMessage = ErrorMessage.ValidationError;
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.ErrorMessage = exception.Message;
                logger.LogError(string.Format(ErrorMessage.ExceptionLoggingMessage, endpoint, response.ErrorMessage));
            }

            response.StatusCode = httpContext.Response.StatusCode;
            response.Data = null;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
