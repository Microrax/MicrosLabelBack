using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LogisTrack.Api.Filter
{
    public class HttpErrorHandlerFilter : IExceptionFilter
    {
        private readonly ILogger<HttpErrorHandlerFilter> _logger;

        public HttpErrorHandlerFilter(ILogger<HttpErrorHandlerFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            if (context.Exception is InvalidOperationException)
            {
                context.Result = new ConflictObjectResult(new { Error = context.Exception.Message });
            }

            if (context.Exception is ArgumentNullException)
            {
                context.Result = new ConflictObjectResult(new { Error = context.Exception.Message });
            }
        }
    }
}
