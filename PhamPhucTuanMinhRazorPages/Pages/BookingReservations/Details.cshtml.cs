using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.BookingReservations
{
    [Authenticated]
    public class DetailsModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IDetailRepository _detailRepository;

        public DetailsModel(IReservationRepository reservationRepository, IDetailRepository detailRepository)
        {
            _reservationRepository = reservationRepository;
            _detailRepository = detailRepository;
        }

        public BookingReservation BookingReservation { get; set; } = new();
        public IList<BookingDetail> Details { get; set; } = new List<BookingDetail>();

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
            Details = _detailRepository.FindBookingDetails(det => det.BookingReservationId == res.BookingReservationId);
            return Page();
        }
    }
}
