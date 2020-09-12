using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareException> _logger;
        private readonly IHostEnvironment _enviroment;
        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger, IHostEnvironment enviroment)
        {
            _enviroment = enviroment;
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
           try
           {
               await _next(context);
           }
           catch(Exception ex)
           {
               _logger.LogError(ex, ex.Message);
               context.Response.ContentType= "application/json";
               context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _enviroment.IsDevelopment()
                ? new ApiException((int)HttpStatusCode.InternalServerError,ex.Message, ex.StackTrace.ToString()):
                 new ApiException((int)HttpStatusCode.InternalServerError);

                 var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                 var json = JsonSerializer.Serialize(response,options);
                 await context.Response.WriteAsync(json);
           }
   
            
        }
    }
}