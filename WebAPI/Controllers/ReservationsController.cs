using System.Net;
using Application.Contracts;
using Application.Features.Commands.CreateReservation;
using Application.Features.Commands.DeleteReservation;
using Application.Features.Commands.UpdateReservation;
using Application.Features.Queries.GetAllReservations;
using Application.Features.Queries.GetReservation;
using Application.Features.Queries.GetReservationByMovieId;
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
    /// Returns all available reservations 
    /// </summary>
    /// <returns>List of <see cref="ReturnReservationDto"/></returns>
    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ReturnReservationDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Reservation with the given id couldn't be found")]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected event occured")]
    public async Task<ActionResult<List<ReturnReservationDto>>> GetAllReservationsAsync()
    {
        var getAllReservationsQuery = new GetAllReservationsQuery();
        var reservation = await _mediator.Send(getAllReservationsQuery);

        if (reservation.Count > 0)
        {
            return Ok(reservation);
        }
        return NotFound();
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
    /// Returns the reservation based on a specified movie ID
    /// </summary>
    /// <param name="movieId">Movie Id</param>
    /// <returns><see cref="ReservationDto"/></returns>
    [HttpGet("get-by-movieid/{movieId:int}")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ReservationDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Reservation with the given id couldn't be found")]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Description = "Validation error")]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected event occured")]
    public async Task<ActionResult<ReturnReservationDto>> GetReservationByMovieIdAsync(int movieId)
    {
        var getReservationByMovieIdQuery = new GetReservationByMovieIdQuery(movieId);
        var reservation = await _mediator.Send(getReservationByMovieIdQuery);

        return Ok(reservation);
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

        var id = await _mediator.Send(createReservationCommand, new CancellationToken());

        return Created("reservations/{id}", id);
    }

    /// <summary>
    /// Finds reservation entity based on Id property and updates entity with new parameters
    /// </summary>
    /// <param name="updateReservationDto"></param>
    /// <returns></returns>
    [HttpPut]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.NotFound)]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Description = "Validation Error")]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> UpdateReservationAsync([FromBody] UpdateReservationDto updateReservationDto)
    {
        var updateReservationCommand = new UpdateReservationCommand(updateReservationDto);
        await _mediator.Send(updateReservationCommand, new CancellationToken());

        return Ok();
    }

    /// <summary>
    /// Deletes reservation based on a given Id
    /// </summary>
    /// <param name="id">Reservation id</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [SwaggerResponse((int)HttpStatusCode.NoContent)]
    [SwaggerResponse((int)HttpStatusCode.NotFound)]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, Description = "Validation Error")]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> DeleteReservationAsync(int id)
    {
        var deleteReservationCommand = new DeleteReservationCommand(id);
        await _mediator.Send(deleteReservationCommand, new CancellationToken());

        return NoContent();
    }
}
