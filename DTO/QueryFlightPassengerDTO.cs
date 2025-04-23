namespace AirlineTicketingAPI.DTOs
{
    public class QueryFlightPassengerDTO : QueryWithPagingDto
    {
        public string FlightNumber { get; set; }
        public DateTime Date { get; set; }
        public new int PageNumber { get; set; } = 1;
    }

    public class PassengerSeatDTO
    {
        public string PassengerName { get; set; }
        public int SeatNumber { get; set; }
    }

    public class PassengerListResultDTO : APIResultDto
    {
        public List <PassengerSeatDTO> Passengers { get; set; } = new();
        public int TotalCount { get; set; }
    }
}