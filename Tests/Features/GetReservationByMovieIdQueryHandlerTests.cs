using Application.Contracts;
using Application.Exceptions;
using Application.Features.Queries.GetReservationByMovieId;
using Application.Interfaces;
using Domain;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Features;

public class GetReservationByMovieIdQueryHandlerTests
{
    private GetReservationByMovieIdQueryHandler _systemUnderTest;
    private Mock<IDataContext> _mockContext;

    [SetUp]
    public void Setup() => _mockContext = new Mock<IDataContext>();

    [Test]
    public async Task Handle_EnterValidMovieId_ShouldReturnReservationAsync()
    {
        // Arrange
        var movieId = 1;
        var reservations = new List<Reservation> { new() { Id = 1, MovieId = movieId, SeatNumbers = 200 } };

        _mockContext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        _systemUnderTest = new GetReservationByMovieIdQueryHandler(_mockContext.Object);

        // Act
        var result = await _systemUnderTest.Handle(new GetReservationByMovieIdQuery(movieId), new CancellationToken());

        // Assert
        result.Should().BeOfType<ReturnReservationDto>();
        result.Should().NotBeNull();
        result.Id.Should().Be(reservations.First().Id);
        result.MovieId.Should().Be(reservations.First().MovieId);
        result.AvailableSeats.Should().Be(reservations.First().SeatNumbers);
    }

    [Test]
    public void Handle_EnterNonExistingMovieId_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var nonExistingMovieId = 1;
        var reservations = new List<Reservation>();

        _mockContext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        _systemUnderTest = new GetReservationByMovieIdQueryHandler(_mockContext.Object);

        // Act & Assert
        _systemUnderTest.Invoking(x =>
                x.Handle(new GetReservationByMovieIdQuery(nonExistingMovieId), new CancellationToken())).Should()
            .ThrowAsync<EntityNotFoundException>();
    }
}
