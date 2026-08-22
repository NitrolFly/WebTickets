namespace WebTickets.Domain.Modules;

public record FileId
{
    public Guid Value { get; }

    private FileId(Guid value) => Value = value;

    public static FileId NewFileId() => new(Guid.NewGuid());
    public static FileId Empty() => new(Guid.Empty);
    public static FileId Create(Guid id) => new(id);
}