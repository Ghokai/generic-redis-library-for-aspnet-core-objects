using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisWithNetCoreSampleApp.Middleware
{
    public static class ErrorHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}