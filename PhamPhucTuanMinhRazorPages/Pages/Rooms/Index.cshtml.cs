using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOs;

namespace PhamPhucTuanMinhRazorPages.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly DAOs.FuminiHotelManagementContext _context;

        public IndexModel(DAOs.FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public IList<RoomInformation> RoomInformation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RoomInformations != null)
            {
                RoomInformation = await _context.RoomInformations
                .Include(r => r.RoomType).ToListAsync();
            }
        }
    }
}
