using CSharpFunctionalExtensions;
namespace WebTickets.Domain.Modules;

public class TicketTag : Shared.Entity<TicketTagId>
{
    private TicketTag(TicketTagId id) : base (id) { }
    private TicketTag(TicketTagId id, TicketId ticketId, TagId tagId) : base(id)
    {
        TicketId = ticketId;
        TagId = tagId;
    }

    public TicketId TicketId { get; private set; }
    
    public virtual Ticket Ticket { get; private set; } = null!;

    public TagId TagId { get; private set; }
    public virtual Tag Tag { get; private set; } = null!;

    public static Result<TicketTag> Create(TicketId ticketId, TagId tagId)
    {
        var id = TicketTagId.NewTagId();
        var ticketTag = new TicketTag(id, ticketId, tagId);
        return Result.Success(ticketTag);
    }
}