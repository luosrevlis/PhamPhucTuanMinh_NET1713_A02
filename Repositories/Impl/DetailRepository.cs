using BusinessObjects;
using DAOs;

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
            return _detailDAO.GetAll().Where(predicate).ToList();
        }
    }
}
