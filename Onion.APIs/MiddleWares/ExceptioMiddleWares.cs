using Azure;
using Onion.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace Onion.APIs.MiddleWares
{
    public class ExceptioMiddleWares
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptioMiddleWares> logger;
        private readonly IHostEnvironment _environment;

        public ExceptioMiddleWares(RequestDelegate Next ,ILogger<ExceptioMiddleWares> Logger, IHostEnvironment environment)
        {
            next = Next;
            logger = Logger;
            _environment = environment;
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
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;


                var Response = (_environment.IsDevelopment()) ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()):new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, null, null);


                var JsonResponse = JsonSerializer.Serialize(Response);
                await context.Response.WriteAsync(JsonResponse);

            }
        }
    }
}
