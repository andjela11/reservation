using Application.Contracts;
using Application.Exceptions;
using Application.Features.Commands.UpdateReservation;
using Application.Interfaces;
using Domain;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Features;

public class UpdateReservationCommandHandlerTests
{
    private UpdateReservationCommandHandler _systemUnderTest;
    private Mock<IDataContext> _mockDataContext;

    [SetUp]
    public void Setup() => _mockDataContext = new Mock<IDataContext>();

    [Test]
    public async Task Handle_UpdateReservationWithGivenParameters_ShouldUpdateReservation()
    {
        // Arrange
        var reservations = new List<Reservation> { new() { Id = 1, MovieId = 1, SeatNumbers = 200 } };

        _mockDataContext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        _systemUnderTest = new UpdateReservationCommandHandler(_mockDataContext.Object);

        var updateReservationDto = new UpdateReservationDto(1, 2, 100);

        // Act
        await _systemUnderTest.Handle(new UpdateReservationCommand(updateReservationDto), new CancellationToken());

        // Assert
        reservations.First().SeatNumbers.Should().Be(updateReservationDto.AvailableSeats);
        reservations.First().MovieId.Should().Be(updateReservationDto.MovieId);
    }

    [Test]
    public void Handle_EnterNonExistingReservationId_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var reservations = new List<Reservation>();

        _mockDataContext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        _systemUnderTest = new UpdateReservationCommandHandler(_mockDataContext.Object);

        var nonExistingId = 2;

        var updateReservationDto = new UpdateReservationDto(nonExistingId, 1, 100);

        // Act & Assert
        _systemUnderTest.Invoking(x =>
            x.Handle(new UpdateReservationCommand(updateReservationDto), new CancellationToken()))
            .Should().ThrowAsync<EntityNotFoundException>();
    }
}
