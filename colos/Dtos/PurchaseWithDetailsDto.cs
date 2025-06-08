namespace colos.Dtos;

public class PurchaseWithDetailsDto
{
    
    public DateTime Date { get; set; }
    
    public decimal Price { get; set; }
    
    public TicketDto Ticket { get; set; }
    
    public ConcertDto Concert { get; set; }
    
}