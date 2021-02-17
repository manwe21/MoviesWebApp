using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;

namespace Web.Attributes
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            if (routeContext.HttpContext.Request.Headers != null)
            {
                if (routeContext.HttpContext.Request.Headers.TryGetValue("X-Requested-With", out var header))
                {
                    if (header.Contains("XMLHttpRequest"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
