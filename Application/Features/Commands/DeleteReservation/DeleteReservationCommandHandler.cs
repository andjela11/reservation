using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.DeleteReservation;

public sealed class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
{
    private readonly IDataContext _context;

    public DeleteReservationCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _context.Reservations
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (reservation is null)
        {
            throw new EntityNotFoundException($"Entity with Id {request.Id} doesn't exist");
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
