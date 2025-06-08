using colos.Dtos;

namespace colos.Services;

public interface IDbService
{

    Task<CustomerWithTicketDto> GetCustomer(int customerId);
    
    Task AddCustomer(AddCustomerRequest request);

}