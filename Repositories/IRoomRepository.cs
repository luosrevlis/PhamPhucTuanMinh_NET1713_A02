using BusinessObjects;

namespace Repositories
{
    public interface IRoomRepository
    {
        List<RoomInformation> GetAllRooms();
        List<RoomInformation> FindRooms(Func<RoomInformation, bool> predicate);
        RoomInformation? FindRoomById(int id);
        void AddRoom(RoomInformation roomInformation);
        void UpdateRoom(RoomInformation roomInformation);
        void DeleteRoom(int id);
    }
}
