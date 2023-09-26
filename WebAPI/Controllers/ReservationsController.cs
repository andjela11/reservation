using System.Net;
using Application.Contracts;
using Application.Features.Commands;
using Application.Features.Commands.CreateReservation;
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

    /// <summary>
    /// Creates the reservation entity based on specified parameter
    /// </summary>
    /// <param name="reservationDto">Object that contains parameters from which new entity is being created</param>
    /// <returns>Id of the created object</returns>
    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.Created)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Description = "Validation Error")]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> CreateReservationAsync([FromBody] CreateReservationDto reservationDto)
    {
        var createReservationCommand = new CreateReservationCommand(reservationDto);

        var id= await this._mediator.Send(createReservationCommand, new CancellationToken());

        if (id > 0)
        {
            return Created("reservations/{id}", id);
        }

        return BadRequest("Something went wrong");
    }
}
