namespace colos.Dtos;

public class AddCustomerRequest
{
    public CustomerDto Customer { get; set; }
    
    public List<PurchaseDto> Purchases { get; set; }
}