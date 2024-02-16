using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using PhamPhucTuanMinhRazorPages.Filters;
using Repositories;

namespace PhamPhucTuanMinhRazorPages.Pages.Rooms
{
    [Admin]
    public class DeleteModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
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
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _roomRepository.DeleteRoom((int)id);
            return RedirectToPage("Index");
        }
    }
}
