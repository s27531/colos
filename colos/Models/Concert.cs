using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace colos.Models;

[Table("Concert")]
public class Concert
{
    
    [Key]
    public int ConcertId { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }
    
    public int AvailableTickets { get; set; }
    
    public ICollection<TicketConcert> TicketConcerts { get; set; }
    
}