using Application.Contracts;
using Application.Features.Queries.GetReservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(int id)
    {
        var getReservationQuery = new GetReservationQuery(id);
        var reservation = await this._mediator.Send(getReservationQuery);

        if (reservation is not null)
        {
            return Ok(reservation);
        }
        return NotFound();
    }
}
