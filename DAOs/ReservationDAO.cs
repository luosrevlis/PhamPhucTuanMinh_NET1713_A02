using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class ReservationDAO : BaseDAO<BookingReservation, int>
    {
        public ReservationDAO(FuminiHotelManagementContext context) : base(context)
        {
        }
    }
}
