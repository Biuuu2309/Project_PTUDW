using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdminAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated || !context.HttpContext.User.IsInRole("Admin"))
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Home", action = "Index" }));
        }
        base.OnActionExecuting(context);
    }
}
