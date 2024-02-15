using BusinessObjects;

namespace Repositories
{
    public interface IDetailRepository
    {
        List<BookingDetail> FindBookingDetails(Func<BookingDetail, bool> predicate);
    }
}
