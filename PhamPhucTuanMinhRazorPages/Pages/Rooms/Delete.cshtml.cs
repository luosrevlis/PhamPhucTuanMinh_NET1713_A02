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
    public class DeleteModel : PageModel
    {
        private readonly DAOs.FuminiHotelManagementContext _context;

        public DeleteModel(DAOs.FuminiHotelManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.RoomInformations == null)
            {
                return NotFound();
            }
            var roominformation = await _context.RoomInformations.FindAsync(id);

            if (roominformation != null)
            {
                RoomInformation = roominformation;
                _context.RoomInformations.Remove(RoomInformation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
