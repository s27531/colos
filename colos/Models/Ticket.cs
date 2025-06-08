using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace colos.Models;

[Table("Ticket")]
public class Ticket
{
    
    [Key]
    public int TicketID { get; set; }
    
    [MaxLength(50)]
    public string SerialNumber { get; set; }
    
    public int SeatNumber { get; set; }
 
    public ICollection<TicketConcert> TicketConcerts { get; set; }
    
}