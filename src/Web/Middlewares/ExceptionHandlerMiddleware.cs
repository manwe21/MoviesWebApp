using Core.Application.Exceptions;
using Core.Application.Exceptions.HttpExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Middlewares
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(options => 
            {
                options.Run(async context => 
                {
                    var ex = context.Features.Get<IExceptionHandlerFeature>();

                    string responseMessage;
                    if (ex.Error is IHttpException exception)
                    {
                        context.Response.StatusCode = exception.StatusCode;
                        responseMessage = ex.Error.Message;
                    }
                    else 
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        responseMessage = "Internal server error";
                    }

                    var responseObject = new {message = responseMessage };
                    string responseBody = JsonConvert.SerializeObject(responseObject);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(responseBody);
                });
            });
        }
    }
}