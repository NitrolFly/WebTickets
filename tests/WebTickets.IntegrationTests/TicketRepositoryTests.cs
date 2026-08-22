using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using WebTickets.Infrastructure;
using WebTickets.IntegrationTests;

public class TicketRepositoryTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16")
        .Build();

    private ApplicationDbContext _context = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_dbContainer.GetConnectionString())
            .UseSnakeCaseNamingConvention()
            .Options;

        _context = new ApplicationDbContext(options);
        await _context.Database.MigrateAsync(); 
        await DatabaseSeeder.SeedAsync(_context, ticketCount: 15);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _dbContainer.DisposeAsync();
    }

    [Fact]
    public async Task Tickets_AreSeededCorrectly()
    {
        var tickets = await _context.Tickets.ToListAsync();

        foreach (var ticket in tickets)
        {
            Console.WriteLine($"322!!!!{ticket.Id.Value}  | {ticket.Title} | {ticket.Status}");
        }

        tickets.Should().HaveCount(15);
    }
}