using Application.Features.Queries.GetReservationByMovieId;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Validators;

public class GetReservationByMovieIdQueryValidatorTests
{
    private GetReservationByMovieIdQueryValidator _validator;

    [SetUp]
    public void Setup() => _validator = new GetReservationByMovieIdQueryValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var getReservationByMovieIdQuery = GetValidPayload();

        // Act
        var result = _validator.Validate(getReservationByMovieIdQuery);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void Validator_InvalidPayload_ShouldBeInvalid()
    {
        // Arrange
        var getReservationByMovieIdQuery = GetValidPayload() with { MovieId = -2 };

        // Act
        var result = _validator.Validate(getReservationByMovieIdQuery);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    private GetReservationByMovieIdQuery GetValidPayload()
    {
        var movieId = 2;
        return new GetReservationByMovieIdQuery(movieId);
    }
}
