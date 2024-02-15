using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class DetailDAO : BaseDAO<BookingDetail, int>
    {
        public DetailDAO(FuminiHotelManagementContext context) : base(context)
        {
        }
    }
}
