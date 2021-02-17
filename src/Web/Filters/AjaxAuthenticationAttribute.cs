using System;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor desc)
            {
                var methodInfo = desc.MethodInfo;
                var attrs = methodInfo.CustomAttributes;
                foreach (var attr in attrs)
                {
                    if (attr.AttributeType == typeof(AllowAnonymousAttribute))
                        return;
                }
            }

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }

            /*var userManager = (UserManager<AppUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<AppUser>));
            var userId = userManager.GetUserId(context.HttpContext.User);
            context.ActionArguments.Add("userId", userId);*/
        }
    }
}
