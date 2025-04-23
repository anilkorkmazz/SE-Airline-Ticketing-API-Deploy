using AirlineTicketingAPI.DTOs;
using AirlineTicketingAPI.Models;
using AirlineTicketingAPI.Repositories;
using AirlineTicketingAPI.Context;

namespace AirlineTicketingAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;
        private readonly TicketAccess _ticketAccess;
 

        public TicketService(AppDbContext context)
        {
            _context = context;
            _ticketAccess = new TicketAccess(context);
        }

        public BuyTicketResultDto BuyTicket(BuyTicketDTO dto)
        {
            var result = new BuyTicketResultDto();

           
           if (dto.PassengerNames == null || dto.PassengerNames.Count == 0)
           {
            result.Status = "FAILURE";
            result.Message = "Passenger list cannot be empty";
            return result;
           }

           //var flight = _flightAccess.GetFlightByNumberAndDate(dto.FlightNumber, dto.Date);

           var flights = _context.Flights
               .Where(f => f.DateFrom.Date == dto.Date.Date)
               .ToList();

            var flight = flights.FirstOrDefault(f =>
                $"FL-{f.Id:D4}" == dto.FlightNumber);


            if (flight == null) 
            {
                result.Status = "FAILURE";
                result.Message = "Flight not found";
                return result;
            }

            int requestedSeats = dto.PassengerNames.Count;
            
            if (flight.AvailableSeats < requestedSeats)
            {
                result.Status = "FAILURE";
                result.Message = "Flight is sold out";
                return result;
            }


            // Her yolcu için bilet oluşturuyoruz
            foreach (var passengerName in dto.PassengerNames)
            {
                var ticketNumber = $"TK-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}";

                var ticket = new Ticket
                {
                    FlightId = flight.Id,
                    PassengerName = passengerName,
                    TicketNumber = ticketNumber,
                    PurchaseDate = DateTime.Now
                };

                _ticketAccess.AddTicket(ticket); 
                result.TicketNumbers.Add(ticketNumber);
            }

            flight.AvailableSeats -= requestedSeats;
            _context.Flights.Update(flight);
            _context.SaveChanges();

            result.Message = "Tickets purchased";
            return result;
        }

        public CheckInResultDTO CheckIn(CheckInDTO dto)
        {
            var result = new CheckInResultDTO();

            var flights = _context.Flights
                .Where(f => f.DateFrom.Date == dto.Date.Date)
                .ToList();

            var flight = flights.FirstOrDefault(f => $"FL-{f.Id:D4}" == dto.FlightNumber);

            if (flight == null)
            {
                result.Status = "FAILURE";
                result.Message = "Flight not found";
                return result;
            }

            var ticket = _ticketAccess.GetTicketByPassengerName(flight.Id, dto.PassengerName);
            if (ticket == null)
            {
                result.Status = "FAILURE";
                result.Message = "Passenger not found";
                return result;
            }

            if (ticket.IsCheckedIn)
            {
                result.Status = "FAILURE";
                result.Message = "Passenger already checked in";
                return result;
            }

            // Seat Number = Yolcunun bu uçuştaki sırası

            /*var seatNumber = _context.Tickets
                .Where(t => t.FlightId == flight.Id && t.Id <= ticket.Id)
                .Count();
            */

            /*
            ticket.IsCheckedIn = true;
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
            */


            var tickets = _ticketAccess.GetTicketsByFlightId(flight.Id);
            var seatNumber = tickets.FindIndex(t => t.Id == ticket.Id) + 1;


            ticket.IsCheckedIn = true;
            _ticketAccess.UpdateTicket(ticket);
            _context.SaveChanges();

            result.SeatNumber = seatNumber;
            result.Status = "SUCCESS";
            result.Message = "Checked in successfully";

            return result;
        }


        public PassengerListResultDTO GetPassengerList(QueryFlightPassengerDTO dto)
        {
            var result = new PassengerListResultDTO();

            var flights = _context.Flights
                .Where(f => f.DateFrom.Date == dto.Date.Date)
                .ToList();

            var flight = flights.FirstOrDefault(f => $"FL-{f.Id:D4}" == dto.FlightNumber);

            if (flight == null)
            {
                result.Status = "FAILURE";
                result.Message = "Flight not found";
                return result;
            }

            /*
            var tickets = _ticketAccess.GetTicketsByFlightId(flight.Id);
            result.TotalCount = tickets.Count;
            */

            // ✅ Sadece Check-In yapılmış yolcuları al
            var checkedInTickets = _ticketAccess
                .GetTicketsByFlightId(flight.Id)
                .Where(t => t.IsCheckedIn)
                .ToList();

            result.TotalCount = checkedInTickets.Count;


            var paginated = checkedInTickets
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();
            
            result.Passengers = paginated.Select((t, index) => new PassengerSeatDTO
            {
                PassengerName = t.PassengerName,
                SeatNumber = ((dto.PageNumber - 1) * dto.PageSize) + index + 1
            }).ToList();

            return result;
        }        
    }
}