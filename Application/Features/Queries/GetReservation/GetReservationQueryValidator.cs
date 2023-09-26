using FluentValidation;

namespace Application.Features.Queries.GetReservation;

public class GetReservationQueryValidator : AbstractValidator<GetReservationQuery>
{
    public GetReservationQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0)
            .WithMessage("Id must be greater than zero");
    }
}
