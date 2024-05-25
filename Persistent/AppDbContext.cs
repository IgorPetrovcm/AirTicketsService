using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketFlight> TicketFlights { get; set; }
    public DbSet<BoardingPass> BoardingPasses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {   }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Seat>()
            .HasKey(s => new { s.AircraftCode, s.SeatNo });

        modelBuilder.Entity<Aircraft>()
            .HasMany(a => a.Seats)
            .WithOne(s => s.Aircraft)
            .HasForeignKey(s => s.AircraftCode);

        modelBuilder.Entity<Airport>()
            .HasMany(a => a.DepartingFlights)
            .WithOne(f => f.Departure)
            .HasForeignKey(f => f.DepartureAirport);

        modelBuilder.Entity<Airport>()
            .HasMany(a => a.ArrivingFlights)
            .WithOne(f => f.Arrival)
            .HasForeignKey(f => f.ArrivalAirport);

        modelBuilder.Entity<Booking>()
            .HasMany(b => b.Tickets)
            .WithOne(t => t.Booking)
            .HasForeignKey(t => t.BookRef);

        modelBuilder.Entity<Ticket>()
            .HasMany(t => t.TicketFlights)
            .WithOne(tf => tf.Ticket)
            .HasForeignKey(tf => tf.TicketNo);

        modelBuilder.Entity<Flight>()
            .HasMany(f => f.TicketFlights)
            .WithOne(tf => tf.Flight)
            .HasForeignKey(tf => tf.FlightId);
        
        modelBuilder.Entity<TicketFlight>()
            .HasKey(f => new {f.FlightId, f.TicketNo});

        modelBuilder.Entity<TicketFlight>()
            .HasOne(tf => tf.Ticket)
            .WithMany(t => t.TicketFlights)
            .HasForeignKey(tf => tf.TicketNo);

        modelBuilder.Entity<TicketFlight>()
            .HasOne(tf => tf.Flight)
            .WithMany(f => f.TicketFlights)
            .HasForeignKey(tf => tf.FlightId);

        modelBuilder.Entity<BoardingPass>()
        .HasKey(bp => new { bp.TicketNo, bp.FlightId });

        // Configuring foreign key relationships
        modelBuilder.Entity<BoardingPass>()
            .HasOne(bp => bp.TicketFlight)
            .WithMany(tf => tf.BoardingPasses) // Assuming TicketFlight has a collection of BoardingPasses
            .HasForeignKey(bp => new { bp.FlightId, bp.TicketNo });
    }
}