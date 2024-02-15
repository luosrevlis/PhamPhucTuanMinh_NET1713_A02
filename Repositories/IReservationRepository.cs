using BusinessObjects;

namespace Repositories
{
    public interface IReservationRepository
    {
        List<BookingReservation> GetAllReservation();
        List<BookingReservation> FindReservations(Func<BookingReservation, bool> predicate);
        BookingReservation? FindReservationById (int id);
        void AddReservation(BookingReservation reservation);
        void UpdateReservation(BookingReservation reservation);
        void DeleteReservation(int id);
    }
}
