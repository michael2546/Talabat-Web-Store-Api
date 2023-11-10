using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate Next , ILogger<ExceptionMiddleWare> logger , IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }

        // Invoke Async

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //prduction => log ex in database
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //if (_env.IsDevelopment())
                //{
                //    var responce = new ApiExeptionResponce((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var responce = new ApiExeptionResponce((int)HttpStatusCode.InternalServerError);
                //}

                var responce = _env.IsDevelopment() ? new ApiExeptionResponce((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) : new ApiExeptionResponce((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(responce);
                context.Response.WriteAsync(JsonResponse);

            }
        }
    }
}
