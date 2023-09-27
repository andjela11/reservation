using Application.Contracts;
using Application.Features.Commands.UpdateReservation;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Validators;

public class UpdateReservationCommandValidatorTests
{
    private UpdateReservationCommandValidator _validator;

    [SetUp]
    public void Setup() => _validator = new UpdateReservationCommandValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var updateReservationCommand = GetValidPayload();

        // Act
        var result = _validator.Validate(updateReservationCommand);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void Validator_InvalidId_ShouldBeInvalid()
    {
        // Arrange
        var updateReservationCommand = GetValidPayload() with { UpdateReservationDto = new UpdateReservationDto(-1, 1, 100) };

        // Act
        var result = _validator.Validate(updateReservationCommand);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Test]
    public void Validator_InvalidParameters_ShouldBeInvalid()
    {
        // Arrange
        var updateReservationCommand = GetValidPayload() with { UpdateReservationDto = new UpdateReservationDto(1, 0, -100) };

        // Act
        var result = _validator.Validate(updateReservationCommand);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    private UpdateReservationCommand GetValidPayload()
    {
        var updateReservationDto = new UpdateReservationDto(1, 1, 100);
        return new UpdateReservationCommand(updateReservationDto);
    }
}
