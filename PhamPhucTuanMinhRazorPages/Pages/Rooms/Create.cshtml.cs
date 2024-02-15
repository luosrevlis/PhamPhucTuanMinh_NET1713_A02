using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DAOs;

namespace PhamPhucTuanMinhRazorPages.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly DAOs.FuminiHotelManagementContext _context;

        public CreateModel(DAOs.FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoomTypeId"] = new SelectList(_context.RoomTypes, "RoomTypeId", "RoomTypeName");
            return Page();
        }

        [BindProperty]
        public RoomInformation RoomInformation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.RoomInformations == null || RoomInformation == null)
            {
                return Page();
            }

            _context.RoomInformations.Add(RoomInformation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
