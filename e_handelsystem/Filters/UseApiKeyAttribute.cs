using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace e_handelsystem.Filters
{
    public class UseApiKeyAttribute: Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKey");

            
            //Headers är till javascript
            if (!context.HttpContext.Request.Headers.TryGetValue("code", out var code))
            {
                context.Result = new UnauthorizedResult();// skickar ett felmeddelade om bad request
                return;
            }

            if (!apiKey.Equals(code))
            {
                context.Result = new UnauthorizedResult();// skickar ett felmeddelade om bad request 
                return;
            }

            await next();


        }
    }
}
