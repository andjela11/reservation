using FluentValidation;

namespace Application.Features.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        When(x => x.ReservationDto is not null, () =>
        {
            RuleFor(x => x.ReservationDto.MovieId).GreaterThan(0);
            RuleFor(x => x.ReservationDto.NumberOfSeats).GreaterThan(0);
        });
    }
}
