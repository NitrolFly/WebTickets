namespace WebTickets.Domain.Modules;

public record TagId
{
    public Guid Value { get; }
    
    private TagId(Guid value)
    {
        Value = value;
    }
    
    public static TagId NewTagId() => new(Guid.NewGuid());
    public static TagId Empty() => new(Guid.Empty);
    public static TagId Create(Guid id) => new(id);
}