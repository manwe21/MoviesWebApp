using System;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Web.Pages.User;

namespace Web.Middlewares
{
    public class RequestInitiatorMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestInitiatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }   

        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager, ILogger<RequestInitiatorMiddleware> logger)
        {
            if (context.Request.Path.Value.Contains("/user/"))
            {
                RequestInitiator initiator = RequestInitiator.Unknown;
                if(context.User.Identity.IsAuthenticated)
                {
                    var querySegments = context.Request.Path.Value.Split("/");
                    if (querySegments.Length > 2)
                    {
                        string ownerName = querySegments[2];
                        var owner = await userManager.FindByNameAsync(ownerName);
                        if (owner == null)
                        {
                            Console.WriteLine(123);
                        }
                        var ownerId = owner.Id;
                        var userId = userManager.GetUserId(context.User);
                        bool isRequestedByOwner = ownerId == userId;
                        initiator = isRequestedByOwner ? RequestInitiator.Owner : RequestInitiator.Guest;
                    }
                }
                
                context.Request.QueryString = context.Request.QueryString.Add("initiator", initiator.ToString());
            }

            await _next.Invoke(context);
        }
    }
}
