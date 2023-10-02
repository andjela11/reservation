using Application.Contracts;
using Application.Features.Commands.CreateReservation;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Validators;

public class CreateReservationCommandValidatorTests
{
    private CreateReservationCommandValidator _validator;

    [SetUp]
    public void Setup() => _validator = new CreateReservationCommandValidator();

    [Test]
    public void CreateReservationCommandValidator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var createReservationCommand = GetValidPayload();

        // Act
        var result = _validator.Validate(createReservationCommand);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void CreateReservationCommandValidator_InvalidPayload_ShouldBeInvalid()
    {
        // Arrange
        var createReservationCommand = GetValidPayload() with { ReservationDto = new CreateReservationDto(0, 0) };

        // Act
        var result = _validator.Validate(createReservationCommand);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Test]
    public void CreateReservationCommandValidator_NegativePayload_ShouldBeInvalid()
    {
        // Arrange
        var createReservationCommand = GetValidPayload() with { ReservationDto = new CreateReservationDto(-1, -3) };

        // Act
        var result = _validator.Validate(createReservationCommand);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Test]
    public void CreateReservationCommandValidator_NegativeMovieId_ShouldBeInvalid()
    {
        // Arrange
        var createReservationCommand = GetValidPayload() with { ReservationDto = new CreateReservationDto(-1, 200) };

        // Act
        var result = _validator.Validate(createReservationCommand);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Test]
    public void CreateReservationCommandValidator_NegativeAvailableSeats_ShouldBeInvalid()
    {
        // Arrange
        var createReservationCommand = GetValidPayload() with { ReservationDto = new CreateReservationDto(1, -200) };

        // Act
        var result = _validator.Validate(createReservationCommand);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    private CreateReservationCommand GetValidPayload()
    {
        var createReservation = new CreateReservationDto(MovieId: 2, NumberOfSeats: 250);
        return new CreateReservationCommand(createReservation);
    }
}
