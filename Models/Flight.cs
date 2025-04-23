using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineTicketingAPI.Models
{
    [Table("Flights")]
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string AirportFrom { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string AirportTo { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int Capacity { get; set; }


        public int AvailableSeats { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
