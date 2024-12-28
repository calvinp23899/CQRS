using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Business.PipelineBehaviors
{
    public class RequestResponseLoggingBehavior<TRequest, TResponse>(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger, IHttpContextAccessor httpContextAccessor) : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var endpoint = httpContext?.Request.Path;
            var method = httpContext?.Request.Method;
            //Request logging
            var requestBody = JsonSerializer.Serialize(request);
            logger.LogInformation($"{DateTimeOffset.UtcNow} - Handling {method} request at {endpoint} with body: {requestBody}");           
            
            //Response logging
            var response = await next();
            var responseBody = JsonSerializer.Serialize(response);
            logger.LogInformation($"{DateTimeOffset.UtcNow} - Handling {method} response at {endpoint} with body: {responseBody}");

            return response;
        }
    }
}
