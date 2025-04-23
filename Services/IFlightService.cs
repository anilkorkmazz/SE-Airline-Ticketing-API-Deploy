using AirlineTicketingAPI.DTOs;

namespace AirlineTicketingAPI.Services
{
    public interface IFlightService
    {
        public AddFlightResultDto AddFlight(AddFlightDto dto);
        
        public FlightQueryResultDto QueryFlights(QueryFlightDto dto);
    }
}
