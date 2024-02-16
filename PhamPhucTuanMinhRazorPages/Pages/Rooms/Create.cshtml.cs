using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using BusinessObjects.Enums;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.Rooms
{
    [Admin]
    public class CreateModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public CreateModel(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
        }

        public IActionResult OnGet()
        {
            ViewData["RoomTypes"] = new SelectList(_roomTypeRepository.GetAllRoomTypes(), "RoomTypeId", "RoomTypeName");
            return Page();
        }

        [BindProperty]
        public RoomInformation RoomInformation { get; set; } = new();

        public IActionResult OnPost()
        {
            if (RoomInformation.RoomPricePerDay == null)
            {
                ModelState.AddModelError("RoomInformation.RoomPricePerDay", "Room price cannot be empty!");
            }
            RoomInformation.RoomType = _roomTypeRepository.GetRoomTypeById(RoomInformation.RoomTypeId)!;
            ModelState.ClearValidationState("RoomInformation");
            if (!TryValidateModel(RoomInformation, "RoomInformation"))
            {
                ViewData["RoomTypes"] = new SelectList(_roomTypeRepository.GetAllRoomTypes(), "RoomTypeId", "RoomTypeName");
                return Page();
            }
            RoomInformation cleanInfo = new()
            {
                RoomNumber = RoomInformation.RoomNumber,
                RoomDetailDescription = RoomInformation.RoomDetailDescription,
                RoomMaxCapacity = RoomInformation.RoomMaxCapacity,
                RoomTypeId = RoomInformation.RoomTypeId,
                RoomType = RoomInformation.RoomType,
                RoomPricePerDay = RoomInformation.RoomPricePerDay,
                RoomStatus = (byte)Status.NotDeleted
            };
            _roomRepository.AddRoom(cleanInfo);
            return RedirectToPage("Index");
        }
    }
}
