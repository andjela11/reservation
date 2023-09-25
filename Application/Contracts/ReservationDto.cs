using Domain;

namespace Application.Contracts;

public record ReservationDto(int MovieId, int AvailableSeats)
{
    public static ReservationDto FromReservation(Reservation reservation)
        => new ReservationDto(reservation.MovieId, reservation.SeatNumbers);
}
