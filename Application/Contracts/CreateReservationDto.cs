using Domain;

namespace Application.Contracts;

public record CreateReservationDto(int MovieId, int NumberOfSeats)
{
    public Reservation ToCreateReservation() => new() { MovieId = MovieId, SeatNumbers = NumberOfSeats };
}
