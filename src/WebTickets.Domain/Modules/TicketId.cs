namespace WebTickets.Domain.Modules;

public record TicketId
{
    private TicketId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
        
    public static TicketId NewTicketId() => new(Guid.NewGuid());
    public static TicketId Empty() => new(Guid.Empty);
    public static TicketId Create(Guid id) => new(id);
}