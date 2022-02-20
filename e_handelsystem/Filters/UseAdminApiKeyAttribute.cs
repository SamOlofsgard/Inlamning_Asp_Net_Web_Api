using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace e_handelsystem.Filters
{
    public class UseAdminApiKeyAttribute: Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("AdminApiKey");

            //Header är inbyggt i HttP requesten i javascripten ex "Code":""  
            if (!context.HttpContext.Request.Headers.TryGetValue("Code", out var code))
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
