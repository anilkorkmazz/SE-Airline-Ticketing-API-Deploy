using AirlineTicketingAPI.Context;
using AirlineTicketingAPI.Models;
using AirlineTicketingAPI.DTOs;

namespace AirlineTicketingAPI.Repositories
{
    public class FlightAccess
    {
        private readonly AppDbContext _context;

        public FlightAccess(AppDbContext context)
        {
            _context = context;
        }

        public int InsertFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            return _context.SaveChanges(); 
        }
        
        public IEnumerable<Flight> QueryFlights(QueryFlightDto dto)
        {
            var result = _context.Flights.Where(f =>
                f.DateFrom >= dto.DateFrom &&
                f.DateTo <= dto.DateTo &&
                f.AirportFrom == dto.AirportFrom &&
                f.AirportTo == dto.AirportTo &&
                f.AvailableSeats >= dto.NumberOfPeople
            );

            return result
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
        }

        public int GetTotalFlightCount(QueryFlightDto dto)
        {
            return _context.Flights.Count(f =>
                f.DateFrom >= dto.DateFrom &&
                f.DateTo <= dto.DateTo &&
                f.AirportFrom == dto.AirportFrom &&
                f.AirportTo == dto.AirportTo &&
                f.AvailableSeats >= dto.NumberOfPeople
            );
        }

        public Flight GetFlightByNumberAndDate(string flightNumber, DateTime date)
        {
            var flights = _context.Flights
                .Where(f => f.DateFrom.Date == date.Date)
                .ToList();
            return flights.FirstOrDefault(f => $"FL-{f.Id:D4}" == flightNumber);
        }

    }
}
