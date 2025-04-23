using AirlineTicketingAPI.DTOs;
using AirlineTicketingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTicketingAPI.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]

    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("buy")]
        [Authorize]
        public BuyTicketResultDto BuyTicket([FromBody] BuyTicketDTO dto)
        {
            return _ticketService.BuyTicket(dto);
        }



        [HttpPost("checkin")]
        [AllowAnonymous]
        public CheckInResultDTO CheckIn([FromBody] CheckInDTO dto)
        {
            return _ticketService.CheckIn(dto);
        }

        [HttpGet("passenger-list/{flightNumber}/{date}/{pageNumber}")]
        [Authorize]
        public PassengerListResultDTO GetPassengerList(string flightNumber, DateTime date,int pageNumber)
        {
            var dto = new QueryFlightPassengerDTO
            {
                FlightNumber = flightNumber,
                Date = date,
                PageNumber = pageNumber
            };

            return _ticketService.GetPassengerList(dto);
        }

    }
}