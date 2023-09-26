using Domain;

namespace Application.Contracts;

public record CreateReservationDto(int MovieId, int AvailableSeats)
{
    public Reservation ToCreateReservation() => new() { MovieId = MovieId, SeatNumbers = AvailableSeats };
}
