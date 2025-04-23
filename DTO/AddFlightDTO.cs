namespace AirlineTicketingAPI.DTOs
{
    public class AddFlightDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public int Duration { get; set; }
        public int Capacity { get; set; }
    }

    public class AddFlightResultDto : APIResultDto
    {
        public int FlightId { get; set; }
    }

    

    public class QueryFlightDto : QueryWithPagingDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public int NumberOfPeople { get; set; }
        public bool IsRoundTrip { get; set; }
    }

    public class FlightDto
    {
        public string FlightNumber { get; set; }
        public int Duration { get; set; }
    }

    public class FlightQueryResultDto : APIResultDto
    {
        public List<FlightDto> Flights { get; set; } = new();
        public int TotalCount { get; set; }
    }


    public class APIResultDto
    {
        public string Status { get; set; } = "SUCCESS";
        public string Message { get; set; }
    }


    public class QueryWithPagingDto
    {
        const int maxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }


    

}
