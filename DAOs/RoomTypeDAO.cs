using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class RoomTypeDAO : BaseDAO<RoomType, int>
    {
        public RoomTypeDAO(FuminiHotelManagementContext context) : base(context)
        {
        }
    }
}
