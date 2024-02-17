using BusinessObjects;
using BusinessObjects.Enums;
using DAOs;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Impl
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDAO _reservationDAO;
        private readonly CustomerDAO _customerDAO;

        public ReservationRepository(ReservationDAO reservationDAO, CustomerDAO customerDAO)
        {
            _reservationDAO = reservationDAO;
            _customerDAO = customerDAO;
        }

        public void AddReservation(BookingReservation reservation)
        {
            _reservationDAO.Add(reservation);
        }

        public void DeleteReservation(int id)
        {
            var res = _reservationDAO.GetById(id);
            if (res == null)
            {
                return;
            }
            res.BookingStatus = (byte)Status.Deleted;
            _reservationDAO.Update(res);
        }

        public BookingReservation? FindReservationById(int id)
        {
            var res = _reservationDAO
                .GetFirst(res => res.BookingReservationId == id && res.BookingStatus != (byte)Status.Deleted);
            if (res == null)
            {
                return null;
            }
            res.Customer = _customerDAO.GetById(res.CustomerId)!;
            return res;
        }

        public List<BookingReservation> FindReservations(Func<BookingReservation, bool> predicate)
        {
            return _reservationDAO.GetAll()
                .Where(res => res.BookingStatus != (byte)Status.Deleted)
                .Include(res => res.Customer)
                .Where(predicate)
                .OrderBy(res => res.Customer.CustomerFullName)
                .ToList();
        }

        public List<BookingReservation> GetAllReservation()
        {
            return _reservationDAO.GetAll()
                .Where(res => res.BookingStatus != (byte)Status.Deleted)
                .Include(res => res.Customer)
                .OrderBy(res => res.Customer.CustomerFullName)
                .ToList();
        }

        public int GetNextId()
        {
            return _reservationDAO.GetAll().Max(res => res.BookingReservationId) + 1;
        }

        public void UpdateReservation(BookingReservation reservation)
        {
            _reservationDAO.Update(reservation);
        }
    }
}
