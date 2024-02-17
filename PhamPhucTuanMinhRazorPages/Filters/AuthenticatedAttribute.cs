using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Enums;

namespace PhamPhucTuanMinhRazorPages.Filters
{
    public class AuthenticatedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32(SessionConst.UserRoleKey) != (int)UserRole.Admin
                && context.HttpContext.Session.GetInt32(SessionConst.UserRoleKey) != (int)UserRole.Customer)
            {
                context.Result = new UnauthorizedResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
