using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands.CreateReservation;

public sealed class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
{
    private readonly IDataContext _context;

    public CreateReservationCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = request.ReservationDto.ToCreateReservation();

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync(cancellationToken);

        return reservation.Id;
    }
}
