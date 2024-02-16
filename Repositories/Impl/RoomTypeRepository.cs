using BusinessObjects;
using DAOs;

namespace Repositories.Impl
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly RoomTypeDAO _roomTypeDAO;

        public RoomTypeRepository(RoomTypeDAO roomTypeDAO)
        {
            _roomTypeDAO = roomTypeDAO;
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return _roomTypeDAO.GetAll().ToList();
        }

        public RoomType? GetRoomTypeById(int id)
        {
            return _roomTypeDAO.GetById(id);
        }
    }
}
