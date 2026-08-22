namespace WebTickets.Domain.Modules;

public record TicketTagId
{
    public Guid Value { get; }
    
    private TicketTagId(Guid value)
    {
        Value = value;
    }
    
    public static TicketTagId NewTagId() => new(Guid.NewGuid());
    public static TicketTagId Empty() => new(Guid.Empty);
    public static TicketTagId Create(Guid id) => new(id);
    
}