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
    public class IndexModel : PageModel
    {
        private readonly DAOs.FuminiHotelManagementContext _context;

        public IndexModel(DAOs.FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public IList<BookingReservation> BookingReservation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BookingReservations != null)
            {
                BookingReservation = await _context.BookingReservations
                .Include(b => b.Customer).ToListAsync();
            }
        }
    }
}
