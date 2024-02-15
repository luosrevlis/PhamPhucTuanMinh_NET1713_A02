using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class CustomerDAO : BaseDAO<Customer, int>
    {
        public CustomerDAO(FuminiHotelManagementContext context) : base(context)
        {
        }
    }
}
