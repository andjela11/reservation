using Application.Contracts;
using Application.Features.Commands.CreateReservation;
using Application.Interfaces;
using Domain;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Features;

public class CreateReservationCommandHandlerTests
{
    private CreateReservationCommandHandler _systemUnderTest;
    private Mock<IDataContext> _mockDataContext;

    [SetUp]
    public void Setup() => _mockDataContext = new Mock<IDataContext>();

    [Test]
    public async Task Handle_CreateReservationWithGivenParameters_ShouldCreateReservation()
    {
        // Arrange
        var reservations = new List<Reservation>();

        _mockDataContext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        _systemUnderTest = new CreateReservationCommandHandler(_mockDataContext.Object);

        var createReservationDto = new CreateReservationDto(2, 200);

        // Act 
        var result = await _systemUnderTest.Handle(new CreateReservationCommand(createReservationDto),
            new CancellationToken());

        // Assert
        result.Should().BeOfType(typeof(int));
        _mockDataContext.Verify(dataContext
            => dataContext.SaveChangesAsync(new CancellationToken()),
            Times.Once());
    }
}
