using LogisTrack.Api.Filter;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace MicrosLabel.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigurationControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(typeof(HttpErrorHandlerFilter));
            })
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
                options.InvalidModelStateResponseFactory = (context) => InvalidModelStateResponse(context);
            });
            
            return services;
        }

        private static IActionResult InvalidModelStateResponse(ActionContext context)
        {
            var result = new BadRequestObjectResult(context.ModelState);

            return result;
        }

        public static IApplicationBuilder UseHttpExceptionHandler(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();

            return app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.ContentType = Text.Plain;

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (exceptionHandlerPathFeature?.Error is InvalidOperationException ||
                        exceptionHandlerPathFeature?.Error is ArgumentException ||
                        exceptionHandlerPathFeature?.Error is FormatException ||
                        exceptionHandlerPathFeature?.Error is NullReferenceException ||
                        exceptionHandlerPathFeature?.Error is ArgumentNullException)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error?.Message);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                });
            });
        }
    }
}
