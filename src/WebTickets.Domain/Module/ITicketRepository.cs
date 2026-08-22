namespace WebTickets.Domain.Modules;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(TicketId id, CancellationToken ct = default);
    Task AddAsync(Ticket ticket, CancellationToken ct = default);
    Task<List<Ticket>> GetByUserIdAsync(UserId userId, CancellationToken ct = default);
}