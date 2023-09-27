using MediatR;

namespace Application.Features.Commands.DeleteReservation;

public record DeleteReservationCommand(int Id) : IRequest;

