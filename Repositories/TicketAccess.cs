using AirlineTicketingAPI.Context;
using AirlineTicketingAPI.Models;
using AirlineTicketingAPI.DTOs;


namespace AirlineTicketingAPI.Repositories
{
    public class TicketAccess
    {
        private readonly AppDbContext _context;

        public TicketAccess(AppDbContext context)
        {
            _context = context;
        }

        public void AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
        }

        public void UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        public Ticket GetTicketByPassengerName(int flightId, string passengerName)
        {
            return _context.Tickets
                .FirstOrDefault(t =>
                    t.FlightId == flightId &&
                    t.PassengerName.ToLower() == passengerName.ToLower());
        }


        public List<Ticket> GetTicketsByFlightId(int flightId)
        {
             return _context.Tickets
                .Where(t => t.FlightId == flightId)
                .OrderBy(t => t.Id)
                .ToList();
        }
    }
}