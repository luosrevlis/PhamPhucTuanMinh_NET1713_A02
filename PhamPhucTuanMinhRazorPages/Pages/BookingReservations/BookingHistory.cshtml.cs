using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using BusinessObjects.Enums;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.BookingReservations
{
    [Customer]
    public class BookingHistoryModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;

        public BookingHistoryModel(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IList<BookingReservation> BookingList { get; set; } = new List<BookingReservation>();

        [BindProperty]
        public DateTime? BookingDateFrom { get; set; }
        [BindProperty]
        public DateTime? BookingDateTo { get; set; }
        [BindProperty]
        public int? PriceFrom { get; set; }
        [BindProperty]
        public int? PriceTo { get; set; }

        public void OnGet()
        {
            int userId = HttpContext.Session.GetInt32(SessionConst.UserIdKey) ?? -1;
            BookingList = _reservationRepository.FindReservations(res => res.CustomerId == userId);
        }

        public void OnPost()
        {
            int userId = HttpContext.Session.GetInt32(SessionConst.UserIdKey) ?? -1;
            BookingList = _reservationRepository.FindReservations(res =>
            {
                if (res.BookingStatus == (byte)Status.Deleted || res.CustomerId != userId)
                {
                    return false;
                }
                if (BookingDateFrom != null && res.BookingDate < BookingDateFrom)
                {
                    return false;
                }
                if (BookingDateTo != null && res.BookingDate > BookingDateTo)
                {
                    return false;
                }
                if (PriceFrom != null && res.TotalPrice < PriceFrom)
                {
                    return false;
                }
                if (PriceTo != null && res.TotalPrice > PriceTo)
                {
                    return false;
                }
                return true;
            });
        }
    }
}
