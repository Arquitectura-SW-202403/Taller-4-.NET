namespace Logica.Services
{
    public class ReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Entidades.Reservation> CreateReservationAsync(Entidades.Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Entidades.Reservation> ChangeReservationAsync(int reservationId, string newReservationTime)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null) return null;

            reservation.ReservationTime = newReservationTime;
            reservation.Status = "changed";
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null) return false;

            reservation.Status = "cancelled";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Entidades.Reservation>> GetReservationsForSpaceAsync(int spaceId)
        {
            return await _context.Reservations.Where(r => r.SpaceId == spaceId).ToListAsync();
        }
    }
}
