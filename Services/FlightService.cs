using AirlineTicketingAPI.DTOs;
using AirlineTicketingAPI.Models;
using AirlineTicketingAPI.Repositories;
using AirlineTicketingAPI.Context;

namespace AirlineTicketingAPI.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightAccess _access;

        public FlightService(AppDbContext context)
        {
            _access = new FlightAccess(context);
        }

        public AddFlightResultDto AddFlight(AddFlightDto dto)
        {
            var result = new AddFlightResultDto();

            try 
            {
                var flight = new Flight
                {
                    DateFrom = dto.DateFrom,
                    DateTo = dto.DateTo,
                    AirportFrom = dto.AirportFrom,
                    AirportTo = dto.AirportTo,
                    Duration = dto.Duration,
                    Capacity = dto.Capacity,
                    AvailableSeats = dto.Capacity
                };

                var success = _access.InsertFlight(flight);

                if (success > 0)
                {
                    result.FlightId = flight.Id;
                    result.Status = "SUCCESS";
                    result.Message = "Flight added successfully.";
                }

                else
                {
                    result.Status = "FAILURE";
                    result.Message = "Could not add flight.";
                }
            }

            catch (Exception ex)
            {
                result.Status = "FAILURE";
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }


        public FlightQueryResultDto QueryFlights(QueryFlightDto dto)
        {
            var result = new FlightQueryResultDto();
            
            try
            {
                var flights = _access.QueryFlights(dto);
                result.TotalCount = _access.GetTotalFlightCount(dto);
                result.Flights = flights.Select(f => new FlightDto
                {
                    FlightNumber = $"FL-{f.Id:D4}",
                    Duration = f.Duration
                }).ToList();
            }
            catch (Exception ex)
            {
                result.Status = "FAILURE";
                result.Message = ex.Message;
            }
            return result;
        }
        
        public Flight GetFlightByNumberAndDate(string flightNumber, DateTime date)
        {
            return _access.GetFlightByNumberAndDate(flightNumber, date);
        }
    }
}
