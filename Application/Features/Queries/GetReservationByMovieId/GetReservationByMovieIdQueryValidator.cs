using FluentValidation;

namespace Application.Features.Queries.GetReservationByMovieId;

public class GetReservationByMovieIdQueryValidator : AbstractValidator<GetReservationByMovieIdQuery>
{
    public GetReservationByMovieIdQueryValidator()
    {
        RuleFor(x => x.MovieId).GreaterThan(0);
    }
}
