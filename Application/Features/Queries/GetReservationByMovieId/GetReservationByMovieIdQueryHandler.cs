using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.GetReservationByMovieId;

public sealed class GetReservationByMovieIdQueryHandler : IRequestHandler<GetReservationByMovieIdQuery, ReturnReservationDto>
{
    private readonly IDataContext _context;

    public GetReservationByMovieIdQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<ReturnReservationDto> Handle(GetReservationByMovieIdQuery request, CancellationToken cancellationToken)
    {
        var reservation =
            await _context.Reservations
                .SingleOrDefaultAsync(x => x.MovieId == request.MovieId, cancellationToken);

        if (reservation is null)
        {
            throw new EntityNotFoundException($"Entity with movieId {request.MovieId} doesn't exist");
        }

        return ReturnReservationDto.FromReservation(reservation);
    }
}
