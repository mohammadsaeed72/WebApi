using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SampleWebApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class tryCatchExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public tryCatchExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext,ILogger<tryCatchExceptionMiddleware> logger)
        {
            try
            {
                return _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                if(ex.InnerException is not null)
                    logger.LogError(ex.InnerException.ToString());

                throw;
            }
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class tryCatchExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UsetryCatchExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<tryCatchExceptionMiddleware>();
        }
    }
}
