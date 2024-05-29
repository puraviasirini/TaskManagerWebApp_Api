using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagerWebApp.API
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger<LogActionFilterAttribute> _logger;
        public LogActionFilterAttribute(ILogger<LogActionFilterAttribute> logger) 
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Executing action: {context.ActionDescriptor.DisplayName}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Executed action: {context.ActionDescriptor.DisplayName}");
        }
    }
}
