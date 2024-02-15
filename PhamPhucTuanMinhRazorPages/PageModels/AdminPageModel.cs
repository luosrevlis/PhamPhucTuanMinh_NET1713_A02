using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Enums;

namespace PhamPhucTuanMinhRazorPages.PageModels
{
    public class AdminPageModel : PageModel
    {
        public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            int role = context.HttpContext.Session.GetInt32(SessionConst.UserRoleKey) ?? 0;
            if (role != (int)UserRole.Admin)
            {
                Unauthorized();
            }
            base.OnPageHandlerSelected(context);
        }
    }
}
