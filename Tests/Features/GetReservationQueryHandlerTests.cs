using Application.Contracts;
using Application.Features.Queries.GetReservation;
using Application.Interfaces;
using Domain;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Features;

public class GetReservationQueryHandlerTests
{
    private GetReservationQueryHandler systemUnderTest;
    private Mock<IDataContext> mockDatacontext;

    [SetUp]
    public void Setup() => this.mockDatacontext = new Mock<IDataContext>();

    [Test]
    public async Task GetReservationQuery_FindById_ShouldReturnReservationAsync()
    {
        // Arrange
        var reservations = new List<Reservation>()
        {
            new() { Id = 1, MovieId = 1, SeatNumbers = 250 }, new() { Id = 2, MovieId = 2, SeatNumbers = 250 }
        };

        this.mockDatacontext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        this.systemUnderTest = new GetReservationQueryHandler(this.mockDatacontext.Object);
        
        // Act
        var result = await this.systemUnderTest.Handle(new GetReservationQuery(2), new CancellationToken());
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ReservationDto>();
        result?.MovieId.Should().Be(2);
        result?.AvailableSeats.Should().Be(250);
    }

    [Test] public void GetReservationQuery_FindByInvalidId_ShouldThrowException()
    {
        // Arrange
        var reservations = new List<Reservation>()
        {
            new() { Id = 1, MovieId = 1, SeatNumbers = 250 }, new() { Id = 2, MovieId = 2, SeatNumbers = 250 }
        };

        this.mockDatacontext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        this.systemUnderTest = new GetReservationQueryHandler(this.mockDatacontext.Object);
        
        // Act & Assert
        this.systemUnderTest.Invoking(x
                => x.Handle(new GetReservationQuery(5), new CancellationToken()))
            .Should().ThrowAsync<Exception>().WithMessage("Entity not found");
    }
    
    [Test] public void GetReservationQuery_FindBySameId_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var reservations = new List<Reservation>()
        {
            new() { Id = 1, MovieId = 1, SeatNumbers = 250 }, new() { Id = 1, MovieId = 2, SeatNumbers = 250 }
        };

        this.mockDatacontext.Setup(x => x.Reservations).ReturnsDbSet(reservations);

        this.systemUnderTest = new GetReservationQueryHandler(this.mockDatacontext.Object);
        
        // Act & Assert
        this.systemUnderTest.Invoking(x
                => x.Handle(new GetReservationQuery(1), new CancellationToken()))
            .Should().ThrowAsync<InvalidOperationException>();
    }
}
