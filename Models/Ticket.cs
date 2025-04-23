using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineTicketingAPI.Models
{
 [Table("Tickets")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int FlightId { get; set; }

        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; }

        [Required]
        [MaxLength(100)]
        public string PassengerName { get; set; }

        [Required]
        [StringLength(12)]
        public string TicketNumber { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        public bool IsCheckedIn { get; set; } = false;
    }
}
