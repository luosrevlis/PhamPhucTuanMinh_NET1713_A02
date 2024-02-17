using BusinessObjects;
using DAOs;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Impl
{
    public class DetailRepository : IDetailRepository
    {
        private readonly DetailDAO _detailDAO;

        public DetailRepository(DetailDAO detailDAO)
        {
            _detailDAO = detailDAO;
        }

        public List<BookingDetail> FindBookingDetails(Func<BookingDetail, bool> predicate)
        {
            return _detailDAO.GetAll()
                .Include(details => details.Room)
                .ThenInclude(room => room.RoomType)
                .Where(predicate)
                .OrderBy(details => details.Room.RoomNumber)
                .ToList();
        }
    }
}
