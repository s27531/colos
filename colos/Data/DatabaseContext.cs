using colos.Models;
using Microsoft.EntityFrameworkCore;

namespace colos.Data;

public class DatabaseContext : DbContext
{
    
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<PurchasedTicket> PurchasedTickets { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketConcert> TicketConcerts { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Concert>().HasData(new List<Concert>
        {
            new() { ConcertId = 1, Name = "Rock conecrt", Date = new DateTime(2025, 6, 8), AvailableTickets = 3},
            new() { ConcertId = 2, Name = "Not Rock conecrt", Date = new DateTime(2025, 6, 9), AvailableTickets = 4},
        });

        modelBuilder.Entity<Ticket>().HasData(new List<Ticket>
        {
            new() { TicketID = 1, SerialNumber = "abcdef123", SeatNumber = 25 },
            new() { TicketID = 2, SerialNumber = "abcdef124", SeatNumber = 50 },
        });

        modelBuilder.Entity<TicketConcert>().HasData(new List<TicketConcert>
        {
            new() { TicketConcertId = 1, TicketId = 2, ConcertId = 1, Price = new decimal(25.99) },
            new() { TicketConcertId = 2, TicketId = 1, ConcertId = 2, Price = new decimal(25.49) },
        });

        modelBuilder.Entity<Customer>().HasData(new List<Customer>
        {
            new() { CustomerId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "555-555-5555" },
            new() { CustomerId = 2, FirstName = "Jane", LastName = "Doe", PhoneNumber = null },
        });

        modelBuilder.Entity<PurchasedTicket>().HasData(new List<PurchasedTicket>
        {
            new() { TicketConcertId = 1, CustomerId = 2, PurchaseDate = new DateTime(2024, 12, 1)},
            new() { TicketConcertId = 2, CustomerId = 1, PurchaseDate = new DateTime(2024, 12, 2)},
        });
    }
    
}