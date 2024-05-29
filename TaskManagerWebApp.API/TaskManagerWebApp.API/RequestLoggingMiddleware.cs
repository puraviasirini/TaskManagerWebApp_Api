namespace TaskManagerWebApp.API
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger) 
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log information before processing the request
            _logger.LogInformation($"Received request: {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            // Log information after processing the request
            _logger.LogInformation($"Request handled: {context.Response.StatusCode}");
        }
    }
}
