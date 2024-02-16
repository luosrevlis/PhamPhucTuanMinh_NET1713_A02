using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Enums;

namespace PhamPhucTuanMinhRazorPages.Filters
{
    public class AdminAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32(SessionConst.UserRoleKey) != (int)UserRole.Admin)
            {
                //context.Result = new UnauthorizedResult();
            }
            base.OnResultExecuting(context);
        }
    }
}
