using BusinessObjects;
using BusinessObjects.Enums;
using DAOs;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Impl
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomDAO _roomDao;
        private readonly RoomTypeDAO _roomTypeDao;

        public RoomRepository(RoomDAO roomDAO, RoomTypeDAO roomTypeDao)
        {
            _roomDao = roomDAO;
            _roomTypeDao = roomTypeDao;
        }

        public void AddRoom(RoomInformation roomInformation)
        {
            _roomDao.Add(roomInformation);
        }

        public void DeleteRoom(int id)
        {
            var room = _roomDao.GetById(id);
            if (room == null)
            {
                return;
            }
            room.RoomStatus = (byte)Status.Deleted;
            _roomDao.Update(room);
        }

        public RoomInformation? FindRoomById(int id)
        {
            var info = _roomDao.GetFirst(info => info.RoomId == id && info.RoomStatus != (byte)Status.Deleted);
            if (info == null)
            {
                return null;
            }
            info.RoomType = _roomTypeDao.GetById(info.RoomTypeId)!;
            return info;
        }

        public List<RoomInformation> FindRooms(Func<RoomInformation, bool> predicate)
        {
            return _roomDao.GetAll()
                .Include(info => info.RoomType)
                .Where(predicate)
                .OrderBy(info => info.RoomNumber)
                .ToList();
        }

        public List<RoomInformation> GetAllRooms()
        {
            return _roomDao.GetAll()
                .Where(info => info.RoomStatus != (byte)Status.Deleted)
                .Include(info => info.RoomType)
                .OrderBy(info => info.RoomNumber)
                .ToList();
        }

        public void UpdateRoom(RoomInformation roomInformation)
        {
            _roomDao.Update(roomInformation);
        }
    }
}
