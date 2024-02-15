using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class RoomDAO : BaseDAO<RoomInformation, int>
    {
        public RoomDAO(DbContext context) : base(context)
        {
        }
    }
}
