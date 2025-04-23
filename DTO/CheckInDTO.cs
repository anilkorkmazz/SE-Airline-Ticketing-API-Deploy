namespace AirlineTicketingAPI.DTOs
{
    public class CheckInDTO
    {
        public string FlightNumber { get; set; }
        public DateTime Date { get; set;}
        public string PassengerName { get; set; }
    }

    public class CheckInResultDTO : APIResultDto
    {
        public int SeatNumber { get; set; }
    }
}