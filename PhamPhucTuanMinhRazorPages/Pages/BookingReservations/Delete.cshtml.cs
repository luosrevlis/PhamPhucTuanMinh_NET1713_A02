using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;
using PhamPhucTuanMinhRazorPages.Filters;

namespace PhamPhucTuanMinhRazorPages.Pages.BookingReservations
{
    [Admin]
    public class DeleteModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteModel(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [BindProperty]
        public BookingReservation BookingReservation { get; set; } = new();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var res = _reservationRepository.FindReservationById((int)id);
            if (res == null)
            {
                return NotFound();
            }
            BookingReservation = res;
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _reservationRepository.DeleteReservation((int)id);
            return RedirectToPage("Index");
        }
    }
}
