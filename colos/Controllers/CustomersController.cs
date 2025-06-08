using colos.Dtos;
using colos.Exceptions;
using colos.Services;
using Microsoft.AspNetCore.Mvc;

namespace colos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(IDbService service) : ControllerBase
    {

        [HttpGet("{customerId}/purchases")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            try
            {
                var customer = await service.GetCustomer(customerId);
                return Ok(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Customer not found");   
            }
        }

        public async Task<IActionResult> AddCustomerWithTickets([FromBody] AddCustomerRequest request)
        {
            try
            {
                await service.AddCustomer(request);
                return Created();
            }
            catch (TicketCountExceededException)
            {
                return BadRequest("Ticket count exceeded");
            }
            catch (CustomerAlreadyExistsException e)
            {
                return BadRequest("Customer already exists");
            }
            catch (ConcertDoesntExistsException e)
            {
                return BadRequest("Concert doesn't exist");
            }
        }
        
    }
}
