using WebTickets.Infrastructure;
using WebTickets.IntegrationTests.Fakers;

namespace WebTickets.IntegrationTests;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, int ticketCount = 10)
    {
        var tickets = TicketFakerFactory.Create().Generate(ticketCount);
        context.Tickets.AddRange(tickets);
        await context.SaveChangesAsync();
    }
}