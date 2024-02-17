using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using BusinessObjects.Enums;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.Rooms
{
    [Admin]
    public class IndexModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;

        public IndexModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IList<RoomInformation> RoomList { get; set; } = new List<RoomInformation>();

        [BindProperty]
        public string? RoomNumber { get; set; }
        [BindProperty]
        public string? RoomDetailDescription { get; set; }
        [BindProperty]
        public int? RoomMaxCapacity { get; set; }
        [BindProperty]
        public int? RoomPriceFrom { get; set; }
        [BindProperty]
        public int? RoomPriceTo { get; set; }


        public void OnGet()
        {
            RoomList = _roomRepository.GetAllRooms();
        }

        public void OnPost()
        {
            RoomList = _roomRepository.FindRooms(room =>
            {
                if (room.RoomStatus == (byte)Status.Deleted)
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(RoomNumber) && !room.RoomNumber.Contains(RoomNumber, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(RoomDetailDescription)
                    && !(room.RoomDetailDescription ?? string.Empty).Contains(RoomDetailDescription, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                if (RoomMaxCapacity != null && room.RoomMaxCapacity != RoomMaxCapacity)
                {
                    return false;
                }
                if (RoomPriceFrom != null && room.RoomPricePerDay < RoomPriceFrom)
                {
                    return false;
                }
                if (RoomPriceTo != null && room.RoomPricePerDay > RoomPriceTo)
                {
                    return false;
                }
                return true;
            });
        }
    }
}
