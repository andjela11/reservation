using Application.Features.Queries.GetReservation;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Validators;

public class GetReservationQueryValidatorTests
{
    private GetReservationQueryValidator _validator;

    [SetUp]
    public void Setup()
    {
        this._validator = new GetReservationQueryValidator();
    }

    [Test]
    public void GetReservationQueryValidator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        GetReservationQuery getReservationQuery = GetValidPayload();
        
        // Act
        var result = this._validator.Validate(getReservationQuery);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Test]
    public void GetReservationQueryValidator_IdIsZero_ShouldBeInvalid()
    {
        // Arrange
        GetReservationQuery getReservationQuery = GetValidPayload() with {Id = 0};
        
        // Act
        var result = this._validator.Validate(getReservationQuery);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Test]
    public void GetReservationQueryValidator_IdIsNegative_ShouldBeInvalid()
    {
        // Arrange
        GetReservationQuery getReservationQuery = GetValidPayload() with {Id = -2};
        
        // Act
        var result = this._validator.Validate(getReservationQuery);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }

    private GetReservationQuery GetValidPayload()
    {
        return new GetReservationQuery(2);
    }
}
