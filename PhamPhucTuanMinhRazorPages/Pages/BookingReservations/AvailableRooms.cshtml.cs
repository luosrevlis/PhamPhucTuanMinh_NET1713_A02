using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using BusinessObjects.Enums;
using PhamPhucTuanMinhRazorPages.Constants;
using PhamPhucTuanMinhRazorPages.Enums;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.BookingReservations
{
    [Authenticated]
    public class AvailableRoomsModel : PageModel
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDetailRepository _detailRepository;
        private readonly IRoomRepository _roomRepository;

        public AvailableRoomsModel(
            IReservationRepository reservationRepository,
            ICustomerRepository customerRepository,
            IDetailRepository detailRepository,
            IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _detailRepository = detailRepository;
            _roomRepository = roomRepository;
        }

        public IList<RoomInformation> RoomList { get; set; } = new List<RoomInformation>();
        [BindProperty]
        public IDictionary<int, bool> SelectedRooms { get; set; } = null!;

        public IActionResult OnGet(int customerId, DateTime stayStart, DateTime stayEnd)
        {
            int userId = HttpContext.Session.GetInt32(SessionConst.UserIdKey) ?? -1;
            int userRole = HttpContext.Session.GetInt32(SessionConst.UserRoleKey) ?? (int)UserRole.None;
            if (userRole == (int)UserRole.Customer && userId != customerId)
            {
                return Unauthorized();
            }

            ViewData["CustomerId"] = customerId;
            HttpContext.Session.SetString(SessionConst.StayStartKey, stayStart.ToString());
            HttpContext.Session.SetString(SessionConst.StayEndKey, stayEnd.ToString());

            var overlaps = _detailRepository
                .FindBookingDetails(det => det.StartDate <= stayStart && stayEnd <= det.EndDate)
                .Select(det => det.RoomId)
                .Distinct()
                .ToList();
            var availableRooms = _roomRepository.GetAllRooms();
            availableRooms.RemoveAll(room => overlaps.Contains(room.RoomId));

            RoomList = availableRooms;
            SelectedRooms = new Dictionary<int, bool>();
            foreach (var room in availableRooms)
            {
                SelectedRooms.Add(room.RoomId, false);
            }
            return Page();
        }

        public IActionResult OnPost(int customerId)
        {
            if (!ExistsBookingDetails())
            {
                return Page();
            }
            DateTime stayStart = DateTime.Parse(HttpContext.Session.GetString(SessionConst.StayStartKey) ?? string.Empty);
            DateTime stayEnd = DateTime.Parse(HttpContext.Session.GetString(SessionConst.StayEndKey) ?? string.Empty);
            int timeByDays = (int)(stayEnd - stayStart).TotalDays;
            decimal totalPrice = 0;

            BookingReservation reservation = new()
            {
                BookingReservationId = _reservationRepository.GetNextId(),
                Customer = _customerRepository.FindCustomerById(customerId)!,
                BookingDate = DateTime.Now,
                BookingStatus = (byte)Status.NotDeleted
            };

            var overlaps = _detailRepository
                .FindBookingDetails(det => det.StartDate <= stayStart && stayEnd <= det.EndDate)
                .Select(det => det.RoomId)
                .Distinct()
                .ToList();
            var availableRooms = _roomRepository.GetAllRooms();
            availableRooms.RemoveAll(room => overlaps.Contains(room.RoomId));

            List<BookingDetail> details = new();
            foreach (var room in availableRooms)
            {
                if (SelectedRooms[room.RoomId])
                {
                    BookingDetail detail = new()
                    {
                        BookingReservationId = reservation.BookingReservationId,
                        Room = room,
                        StartDate = stayStart,
                        EndDate = stayEnd,
                        ActualPrice = room.RoomPricePerDay * timeByDays
                    };
                    details.Add(detail);
                    totalPrice += detail.ActualPrice ?? 0;
                }
            }
            reservation.BookingDetails = details;
            reservation.TotalPrice = totalPrice;
            _reservationRepository.AddReservation(reservation);
            return RedirectToPage("Index");
        }

        private bool ExistsBookingDetails()
        {
            foreach (var pair in SelectedRooms)
            {
                if (pair.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
