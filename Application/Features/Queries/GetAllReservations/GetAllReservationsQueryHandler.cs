using Application.Contracts;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.GetAllReservations;

public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, List<ReturnReservationDto>>
{
    private IDataContext _context;

    public GetAllReservationsQueryHandler(IDataContext context)
    {
        _context = context;
    }
    
    public async Task<List<ReturnReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _context.Reservations.ToListAsync(cancellationToken);

        return reservations.Select(ReturnReservationDto.FromReservation).ToList();
    }
}
