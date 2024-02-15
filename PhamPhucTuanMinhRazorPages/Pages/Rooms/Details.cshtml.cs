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
    public class DetailsModel : PageModel
    {
        private readonly DAOs.FuminiHotelManagementContext _context;

        public DetailsModel(DAOs.FuminiHotelManagementContext context)
        {
            _context = context;
        }

      public RoomInformation RoomInformation { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RoomInformations == null)
            {
                return NotFound();
            }

            var roominformation = await _context.RoomInformations.FirstOrDefaultAsync(m => m.RoomId == id);
            if (roominformation == null)
            {
                return NotFound();
            }
            else 
            {
                RoomInformation = roominformation;
            }
            return Page();
        }
    }
}
