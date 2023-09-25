using Application.Contracts;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.GetReservation;

public sealed class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, ReservationDto?>
{
    private readonly IDataContext _context;

    public GetReservationQueryHandler(IDataContext context)
    {
        this._context = context;
    }
    
    public async Task<ReservationDto?> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservation = await this._context.Reservations
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation is null)
        {
            throw new Exception("Entity not found");
        }

        return ReservationDto.FromReservation(reservation);
    }
}
