using AirlineTicketingAPI.DTOs;
using AirlineTicketingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTicketingAPI.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost]
        [Authorize]
        public AddFlightResultDto AddFlight([FromBody] AddFlightDto dto)
        {
            return _flightService.AddFlight(dto);
        }

        [HttpPost("query")]
        [AllowAnonymous]
        public FlightQueryResultDto QueryFlights([FromBody] QueryFlightDto dto)
        {
            return _flightService.QueryFlights(dto);
        }
    }
}
