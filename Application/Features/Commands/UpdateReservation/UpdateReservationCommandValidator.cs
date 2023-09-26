using FluentValidation;

namespace Application.Features.Commands.UpdateReservation;

public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
{
    public UpdateReservationCommandValidator()
    {
        When(x => x.UpdateReservationDto is not null, () =>
        {
            RuleFor(x => x.UpdateReservationDto.Id).NotEmpty();
            RuleFor(x => x.UpdateReservationDto.Id).GreaterThan(0);
            
            RuleFor(x => x.UpdateReservationDto.MovieId).NotEmpty();
            RuleFor(x => x.UpdateReservationDto.MovieId).GreaterThan(0);
            
            RuleFor(x => x.UpdateReservationDto.AvailableSeats).NotEmpty();
            RuleFor(x => x.UpdateReservationDto.AvailableSeats).GreaterThan(0);
        });
    }
}
