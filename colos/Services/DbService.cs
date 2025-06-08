using colos.Data;
using colos.Dtos;
using colos.Exceptions;
using colos.Models;
using Microsoft.EntityFrameworkCore;

namespace colos.Services;

public class DbService(DatabaseContext context) : IDbService
{
    
    public async Task<CustomerWithTicketDto> GetCustomer(int customerId)
    {
        var customer = await context.Customers
            .Where(c => c.CustomerId == customerId)
            .Select(c => new CustomerWithTicketDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Purchases = c.PurchasedTickets.Select(pt => new PurchaseWithDetailsDto
                {
                    Date = pt.PurchaseDate,
                    Price = pt.TicketConcert.Price,
                    Ticket = new TicketDto
                    {
                      Serial = pt.TicketConcert.Ticket.SerialNumber,
                      SeatNumber  = pt.TicketConcert.Ticket.SeatNumber,
                    },
                    Concert = new ConcertDto
                    {
                        Name = pt.TicketConcert.Concert.Name,
                        Date = pt.TicketConcert.Concert.Date,
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (customer == null)
        {
            throw new KeyNotFoundException("Customer not found");
        }
        return customer;
    }

    public async Task AddCustomer(AddCustomerRequest request)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var customerExists = await context.Customers
                .AnyAsync(c => c.CustomerId == request.Customer.Id);
            if (customerExists)
            {  
               throw new CustomerAlreadyExistsException(); 
            }
    
            var ticketCount = request.Purchases.Count;
            if (ticketCount > 5)
            {
                throw new TicketCountExceededException();
            }
    
            var newCustomer = new Customer
            {
                //CustomerId = request.Customer.Id, breaks the program
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                PhoneNumber = request.Customer.PhoneNumber,
            };
            await context.Customers.AddAsync(newCustomer);
            await context.SaveChangesAsync();
    
            foreach (var p in request.Purchases)
            {
                var newTicket = new Ticket
                {
                    SerialNumber = "[SERIAL NUMBER]",
                    SeatNumber = p.SeatNumber,
                };
                await context.Tickets.AddAsync(newTicket);
                await context.SaveChangesAsync();
                
                var concert = await context.Concerts
                    .Where(c => c.Name == p.ConcertName)
                    .FirstOrDefaultAsync();
                if (concert == null)
                {
                    throw new ConcertDoesntExistsException();
                }
    
                var tc = new TicketConcert
                {
                    TicketId = newTicket.TicketID,
                    ConcertId = concert.ConcertId,
                    Price = p.Price,
                };
                await context.TicketConcerts.AddAsync(tc);
                await context.SaveChangesAsync();
    
                var pt = new PurchasedTicket
                {
                    TicketConcertId = tc.TicketConcertId,
                    CustomerId = newCustomer.CustomerId,
                    PurchaseDate = DateTime.Now,
                };
                await context.PurchasedTickets.AddAsync(pt);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
}