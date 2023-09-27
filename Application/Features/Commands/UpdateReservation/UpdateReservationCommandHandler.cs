﻿using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Commands.UpdateReservation;

public sealed class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
{
    private readonly IDataContext _context;

    public UpdateReservationCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation =
            await _context.Reservations
                .SingleOrDefaultAsync(x => x.Id == request.UpdateReservationDto.Id, cancellationToken);
        if (reservation is null)
        {
            throw new EntityNotFoundException($"Entity with Id {request.UpdateReservationDto.Id} doesn't exist");
        }

        reservation.MovieId = request.UpdateReservationDto.MovieId;
        reservation.SeatNumbers = request.UpdateReservationDto.AvailableSeats;

        _context.Reservations.Update(reservation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
