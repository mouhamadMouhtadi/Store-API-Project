using System.Net;
using System.Text.Json;
using Microsoft.VisualBasic;
using Store.Service.HandleResponse;

namespace Store.Web.Middleware
{
    public class ExceptionMiddelware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddelware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddelware(RequestDelegate Next , ILogger<ExceptionMiddelware> logger, IHostEnvironment env)
        {
            next = Next;
            this.logger = logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                // production => log database

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var ResponseEnv = env.IsDevelopment() ? new CustomException(500, ex.Message, ex.StackTrace.ToString())
                    : new CustomException((int)HttpStatusCode.InternalServerError);
                var Options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonResponse = JsonSerializer.Serialize(ResponseEnv, Options);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
