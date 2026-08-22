using Bogus;
using WebTickets.Domain.Modules;

namespace WebTickets.IntegrationTests.Fakers;

public static class TicketFakerFactory
{
    public static Faker<Ticket> Create() =>
        new Faker<Ticket>()
            .UseSeed(42)
            .CustomInstantiator(f => Ticket.Create(
                TicketId.NewTicketId(),
                f.Lorem.Sentence(4),
                f.Lorem.Paragraph()
            ).Value);
}