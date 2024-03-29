﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using BusinessObjects.Enums;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.BookingReservations
{
    [Admin]
    public class IndexModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;

        public IndexModel(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IList<BookingReservation> BookingList { get; set; } = new List<BookingReservation>();

        [BindProperty]
        public string? CustomerName { get; set; }
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
            BookingList = _reservationRepository.GetAllReservation();
        }

        public void OnPost()
        {
            BookingList = _reservationRepository.FindReservations(res =>
            {
                if (res.BookingStatus == (byte)Status.Deleted)
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(CustomerName)
                    && !(res.Customer.CustomerFullName ?? string.Empty).Contains(CustomerName, StringComparison.OrdinalIgnoreCase))
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
