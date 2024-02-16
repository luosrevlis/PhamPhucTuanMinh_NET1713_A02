using BusinessObjects;

namespace Repositories
{
    public interface IRoomTypeRepository
    {
        List<RoomType> GetAllRoomTypes();
        RoomType? GetRoomTypeById(int id);
    }
}
