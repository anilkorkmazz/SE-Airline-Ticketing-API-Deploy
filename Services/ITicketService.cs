using AirlineTicketingAPI.DTOs;


namespace AirlineTicketingAPI.Services
{
    public interface ITicketService
    {
        BuyTicketResultDto BuyTicket(BuyTicketDTO dto);
        
        CheckInResultDTO CheckIn(CheckInDTO dto);

        PassengerListResultDTO GetPassengerList(QueryFlightPassengerDTO dto);

    }
}