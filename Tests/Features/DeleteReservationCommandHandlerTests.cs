using Application.Features.Commands.DeleteReservation;
using Application.Interfaces;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Features;

public class DeleteReservationCommandHandlerTests
{
    private DeleteReservationCommandHandler _systemUnderTest;
    private Mock<IDataContext> _mockContext;

    [SetUp]
    public void Setup() => _mockContext = new Mock<IDataContext>();

    [Test]
    public async Task Handle_DeleteReservationWithValidId_ShouldDeleteReservation()
    {
        // Arrange
        var idToDelete = 1;
        var reservations = new List<Reservation> { new() { Id = idToDelete, MovieId = 1, SeatNumbers = 100 } };

        _mockContext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        _systemUnderTest = new DeleteReservationCommandHandler(_mockContext.Object);

        // Act
        await _systemUnderTest.Handle(new DeleteReservationCommand(idToDelete), new CancellationToken());

        // Assert
        _mockContext.Verify(p => p.Reservations.Remove(reservations.First()), Times.Once);
        _mockContext.Verify(context => context.SaveChangesAsync(new CancellationToken()), Times.Once);
    }
}
