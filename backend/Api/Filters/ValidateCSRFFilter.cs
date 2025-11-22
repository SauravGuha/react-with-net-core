
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class ValidateCSRFFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the action itself is marked with [AllowAnonymous]
            var actionAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>()
                .Any();

            // Check if the controller itself is marked with [AllowAnonymous]
            var controllerAllowAnonymous = context.Controller.GetType()
            .GetCustomAttributes(typeof(AllowAnonymousAttribute), true)
            .Any();

            if (actionAllowAnonymous || controllerAllowAnonymous)
                return;

            if (context.HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase)
            || context.HttpContext.Request.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase)
            || context.HttpContext.Request.Method.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
            {
                var token = context.HttpContext.Request.Cookies["x-csrf-token"]?.ToString() ?? "";
                var tokenHeader = context.HttpContext.Request.Headers["reactivitycsrftoken"].ToString();
                if (tokenHeader != token)
                {
                    context.Result = new BadRequestResult();
                }
            }
        }
    }
}