using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Enums;

namespace PhamPhucTuanMinhRazorPages.Filters
{
    public class CustomerAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32(SessionConst.UserRoleKey) != (int)UserRole.Customer)
            {
                context.Result = new UnauthorizedResult();
            }
            base.OnResultExecuting(context);
        }
    }
}
