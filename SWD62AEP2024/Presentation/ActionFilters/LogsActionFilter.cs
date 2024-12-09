using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters
{
    public class LogsActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            Log myLog = new Log();
            myLog.Message = $"Timestamp: {DateTime.Now}, Action: {context.HttpContext.Request.Method}, " + $"QueryString: {context.HttpContext.Request.QueryString}";

            myLog.User = "Anonymous user";
            var user = context.HttpContext.User;
            if (user != null)
            {
                if (user.Identity.IsAuthenticated)
                {
                    myLog.User = user.Identity.Name;
                }
            }

            myLog.IPAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            LogsRepository logsRepository = context.HttpContext.RequestServices.GetService<LogsRepository>();
            logsRepository.AddLog(myLog);

            base.OnActionExecuting(context); // If you want to keep running the next code smoothly, do not delete this line.
        }
    }
}
