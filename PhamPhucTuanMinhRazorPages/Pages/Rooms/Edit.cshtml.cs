using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.Rooms
{
    [Admin]
    public class EditModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public EditModel(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
        }

        [BindProperty]
        public RoomInformation RoomInformation { get; set; } = new();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var room = _roomRepository.FindRoomById((int)id);
            if (room == null)
            {
                return NotFound();
            }
            RoomInformation = room;
            ViewData["RoomTypeId"] = new SelectList(_roomTypeRepository.GetAllRoomTypes(), "RoomTypeId", "RoomTypeName");
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
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
            var roomToUpdate = _roomRepository.FindRoomById((int)id);
            if (roomToUpdate == null)
            {
                return NotFound();
            }
            roomToUpdate.RoomNumber = RoomInformation.RoomNumber;
            roomToUpdate.RoomDetailDescription = RoomInformation.RoomDetailDescription;
            roomToUpdate.RoomMaxCapacity = RoomInformation.RoomMaxCapacity;
            roomToUpdate.RoomTypeId = RoomInformation.RoomTypeId;
            roomToUpdate.RoomType = RoomInformation.RoomType;
            roomToUpdate.RoomPricePerDay = RoomInformation.RoomPricePerDay;
            _roomRepository.UpdateRoom(roomToUpdate);
            return RedirectToPage("Index");
        }
    }
}
