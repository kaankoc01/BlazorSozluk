using BlazorSozluk.Common.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using BlazorSozluk.Common.Infrastructure.Results;

namespace BlazorSozluk.Api.WebApi.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
            bool includeExceptionDetails = false,
            bool useDefaultHandLingResponse = true,
            Func<HttpContext, Exception, Task> handleException = null)
        {
            app.UseExceptionHandler(optios  =>
            {
                optios.Run(context =>
                {
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                    if (!useDefaultHandLingResponse && handleException == null)
                        throw new ArgumentNullException(nameof(handleException),
                            $"{nameof(handleException)} cannot be null when {nameof(useDefaultHandLingResponse)} is false");

                    if (!useDefaultHandLingResponse && handleException != null)
                        return handleException(context, exceptionObject.Error);

                    return DefaultHandleException(context, exceptionObject.Error, includeExceptionDetails);
                });
            });


            return app;
        }

        private static async Task DefaultHandleException(HttpContext context, Exception exception, bool includeExceptionDetails)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Internal Server Error Occured!";

            if (exception is UnauthorizedAccessException)
                statusCode = HttpStatusCode.Unauthorized;

            if (exception is DatabaseValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                var validationResponse = new ValidationResponseModel(exception.Message);
                await WriteResponse(context, statusCode, validationResponse);
                return;
            }

            var res = new
            {
                HttpStatusCode = (int)statusCode,
                Detail = includeExceptionDetails ? exception.ToString() : message
            };

            await WriteResponse(context, statusCode, res);
        }

        private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObject)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(responseObject);
        }
    }
}