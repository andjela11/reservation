using Domain;

namespace Application.Contracts;

public record ReturnReservationDto(int Id, int MovieId, int AvailableSeats)
{
    public static ReturnReservationDto FromReservation(Reservation reservation) =>
        new ReturnReservationDto(reservation.Id, reservation.MovieId, reservation.SeatNumbers);
};
