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
            catch (KeyNotFoundException e)
            {
                return NotFound();   
            }
        }

        public async Task<IActionResult> AddCustomerWithTickets([FromBody] AddCustomerRequest request)
        {
            try
            {
                await service.AddCustomer(request);
                return Created();
            }
            catch (TicketCountExceededException e)
            {
                return BadRequest(e.Message);
            }
            catch (CustomerAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (ConcertDoesntExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
