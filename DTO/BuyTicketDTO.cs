namespace AirlineTicketingAPI.DTOs
{
    public class BuyTicketDTO
    {
        public string FlightNumber { get; set; }
        public DateTime Date { get; set; }
        public List<string> PassengerNames { get; set; }
    }

    public class BuyTicketResultDto : APIResultDto
    {
        public List<string> TicketNumbers { get; set; } = new();
    }
    
}