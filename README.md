#  Airline Ticketing API 


##  Design, Assumptions, and Issues Encountered

---

###  Design Decisions

- The project is structured based on **Service-Oriented Architecture (SOA)**.
- All database operations are handled through **Access layers** (`FlightAccess`, `TicketAccess`), while business logic is managed in **Service layers** (`FlightService`, `TicketService`).
- API endpoints are implemented via **Controllers**, using **DTO** objects for input/output.
- All endpoints are tested and accessible via **Swagger UI**.
- **API versioning** is applied using the `/v1` prefix.
- **Paging** is handled using a reusable `QueryWithPagingDto` base class.
- All responses use a consistent `APIResultDto` structure for status messaging.

---

###  Assumptions 

1. **Authentication:**
   - Authentication is simulated via a constant user; no real user DB validation is performed.
   - **JWT-based authentication** is applied only to restricted endpoints like `BuyTicket`, `AddFlight`, and `QueryFlightPassengerList`.

2. **Flight Numbers:**
   - Flight numbers are dynamically generated as `FL-{id:D4}` based on the DB ID and are not stored explicitly in the database.

3. **Flight Scheduling:**
   - Multiple flights can be scheduled with the same route and time; the system does not enforce conflict validation.
   - The `isRoundTrip` parameter is accepted but not structurally implemented—used only as a filter.

4. **Ticketing and Capacity:**
   - `BuyTicket` supports bulk ticket purchase in a single request.
   - System checks available capacity before booking and returns `"Flight is sold out"` if insufficient.
   - Each ticket is assigned a unique code like `TK-XXXXXX`.

5. **Check-In Process:**
   - Seats are assigned sequentially based on ticket order.
   - Duplicate check-ins are blocked with a `"Passenger already checked in"` error.

6. **Paging:**
   - Paging is enforced in all list endpoints, with a max `PageSize = 50`.
   - In `QueryFlightPassengerList`, paging is inherited from `QueryWithPagingDto`.
   - On Swagger UI, `FlightNumber`, `Date`, and `PageNumber` are visible for GET operations, `PageSize` defaults to 10.

7. **Endpoint Design:**
   - RESTful principles are followed; all endpoints are versioned under `/v1`.
   - `POST`: AddFlight, BuyTicket, CheckIn  
     `GET`: QueryFlight, GetPassengerList  
   - `GetPassengerList` endpoint:  
     `GET /v1/ticket/passenger-list/{flightNumber}/{date}/{pageNumber}`

8. **Filtering and Realtime Data:**
   - `QueryFlight` only lists flights with available seats.
   - All queries reflect real-time data from the database.

---

###  Issues Encountered

- Persistent connection issues occurred with **Azure Web App Service** and **Azure MySQL Flexible Server**.
- Faced frequent errors such as:
  - `Access Denied`
  - `SSL Handshake Failed`
  - IP-based restrictions (Firewall rules)
- Even with correctly configured connection strings in Azure App Settings, database connectivity failed.
- Due to MySQL access failures, alternative platforms like **Render.com** were considered.
- **Azure region limitations** prevented some resources from being created, slowing down deployment and testing.

---


##  Data Model

This project uses a relational data model to manage flights and tickets within the airline ticketing system. The core entities and their relationships are described below:

---

###  Flight Table

Stores individual flight information such as schedule, route, duration, and seat capacity.

**Fields:**

- `id`: Primary key.
- `dateFrom`: Flight departure date.
- `dateTo`: Flight arrival date.
- `airportFrom`: Departure airport code.
- `airportTo`: Arrival airport code.
- `duration`: Flight duration in minutes.
- `capacity`: Total seat capacity of the flight.
- `availableSeats`: Remaining available seats.

---

###  Ticket Table

Stores ticket information for each passenger.  
Linked to a specific flight via a foreign key.

**Fields:**

- `id`: Primary key.
- `flightId`: Foreign key referencing the related flight.
- `passengerName`: Name of the passenger.
- `ticketNumber`: Unique ticket identifier.
- `purchaseDate`: Date when the ticket was bought.
- `isCheckedIn`: Boolean indicating whether the passenger checked in.

---

###  Relationships

- **One-to-Many**: A single flight can have many tickets (Flight 1:N Ticket).
- Each ticket belongs to one specific flight.

---

###  ER Diagram

The Entity-Relationship (ER) diagram below illustrates the structure and connections between the Flight and Ticket tables:


![ER Diagram](https://github.com/user-attachments/assets/c4198aee-24fe-422f-9e7e-56fe5d10c4dd)


---

###  Demo Video

> 🔗 https://youtu.be/ok-9YK0i7Oc

---



##  Swagger Link


> 🔗 https://anil-airline-api.azurewebsites.net/swagger/index.html

---
