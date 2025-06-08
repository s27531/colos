namespace colos.Dtos;

public class CustomerWithTicketDto
{
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public List<PurchaseWithDetailsDto> Purchases { get; set; }
    
}