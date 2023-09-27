using Application.Contracts;
using MediatR;

namespace Application.Features.Commands.UpdateReservation;

public record UpdateReservationCommand(UpdateReservationDto UpdateReservationDto) : IRequest;
