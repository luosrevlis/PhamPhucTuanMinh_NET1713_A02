using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class DetailDAO : BaseDAO<BookingDetail, int>
    {
        public DetailDAO(DbContext context) : base(context)
        {
        }
    }
}
