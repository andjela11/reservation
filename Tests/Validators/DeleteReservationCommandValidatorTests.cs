using Application.Features.Commands.DeleteReservation;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Validators;

public class DeleteReservationCommandValidatorTests
{
    private DeleteReservationCommandValidator _validator;

    [SetUp]
    public void Setup() => _validator = new DeleteReservationCommandValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var deleteReservationCommand = GetValidPayload();
        
        // Act
        var result = _validator.Validate(deleteReservationCommand);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void Validator_InvalidPayload_ShouldBeInvalid()
    {
        // Arrange
        var deleteReservationCommand = GetValidPayload() with { Id = -1 };
        
        // Act 
        var result = _validator.Validate(deleteReservationCommand);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    private DeleteReservationCommand GetValidPayload()
    {
        var id = 1;
        var deleteReservationCommand = new DeleteReservationCommand(id);

        return deleteReservationCommand;
    }
}
