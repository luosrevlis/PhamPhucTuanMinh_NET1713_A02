using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Enums;

namespace PhamPhucTuanMinhRazorPages.PageModels
{
    public class CustomerPageModel : PageModel
    {
        public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            int role = context.HttpContext.Session.GetInt32(SessionConst.UserRoleKey) ?? 0;
            if (role != (int)UserRole.Customer)
            {
                Unauthorized();
            }
            base.OnPageHandlerSelected(context);
        }
    }
}
