using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOs;

namespace PhamPhucTuanMinhRazorPages.Pages.BookingReservations
{
    public class DeleteModel : PageModel
    {
        private readonly DAOs.FuminiHotelManagementContext _context;

        public DeleteModel(DAOs.FuminiHotelManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookingReservation BookingReservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BookingReservations == null)
            {
                return NotFound();
            }

            var bookingreservation = await _context.BookingReservations.FirstOrDefaultAsync(m => m.BookingReservationId == id);

            if (bookingreservation == null)
            {
                return NotFound();
            }
            else 
            {
                BookingReservation = bookingreservation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BookingReservations == null)
            {
                return NotFound();
            }
            var bookingreservation = await _context.BookingReservations.FindAsync(id);

            if (bookingreservation != null)
            {
                BookingReservation = bookingreservation;
                _context.BookingReservations.Remove(BookingReservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
