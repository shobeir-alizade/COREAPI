using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Framework.Infrastructure.Filters
{
    public class LogFilterAttribute : Attribute, IActionFilter,IAsyncActionFilter
    {
        private readonly ILogger<LogFilterAttribute> _logger = null;

        public LogFilterAttribute(ILogger<LogFilterAttribute> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Action " + context.ActionDescriptor.DisplayName + " is Executing ");

           var res= await next();
          
         
       
            _logger.LogInformation("Action " + context.ActionDescriptor.DisplayName + "  Executed ");

        }
    }
}
