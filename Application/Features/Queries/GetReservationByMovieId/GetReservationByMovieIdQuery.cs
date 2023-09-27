using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.GetReservationByMovieId;

public record GetReservationByMovieIdQuery(int MovieId) : IRequest<ReturnReservationDto>;
