using System.Net;
using Application.Contracts;
using Application.Features.Queries.GetReservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers;

/// <summary>
/// Basic CRUD operations on Reservation entity
/// </summary>
[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Returns the reservation based on a specified ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="ReservationDto"/></returns>
    [HttpGet("{id:int}")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ReservationDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Reservation with the given id couldn't be found")]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Description = "Validation error")]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected event occured")]
    public async Task<ActionResult<ReservationDto>> GetReservationAsync(int id)
    {
        var getReservationQuery = new GetReservationQuery(id);
        var reservation = await _mediator.Send(getReservationQuery);

        if (reservation is not null)
        {
            return Ok(reservation);
        }
        return NotFound();
    }
}
