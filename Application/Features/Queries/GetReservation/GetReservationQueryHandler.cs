using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.GetReservation;

public sealed class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, ReservationDto?>
{
    private readonly IDataContext _context;

    public GetReservationQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<ReservationDto?> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _context.Reservations
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation is null)
        {
            throw new EntityNotFoundException("Entity not found");
        }

        return ReservationDto.FromReservation(reservation);
    }
}
