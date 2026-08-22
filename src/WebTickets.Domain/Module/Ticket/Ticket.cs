using CSharpFunctionalExtensions;
using WebTickets.Domain.Shared;

namespace WebTickets.Domain.Modules;

public class Ticket: TicketTaggableEntity<TicketId>
{
    private Ticket(TicketId id) : base(id) { }
    
    private Ticket(TicketId ticketId, string title, string description) : base(ticketId)
    {
        Title = title;
        Description = description;
    }
    
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    
    public TicketStatus Status { get; set; }  
    public TicketPriority Priority { get; set; }
    
    public enum TicketStatus
    {
        Pending = 0, // Ожидает ответа
        InProgress = 1, // В работе
        Completed = 2 // Выполнен
    }

    public enum TicketPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Critical = 3
    }
    
    public static Result<Ticket> Create(TicketId ticketId, string title, string description)
    {
        if(string.IsNullOrWhiteSpace(title))
           return Result.Failure<Ticket>("Title can not be empty");
        
        if(string.IsNullOrWhiteSpace(description))
            return Result.Failure<Ticket>("Description can not be empty");

        var ticket = new Ticket(ticketId,title, description);

        return Result.Success(ticket);
    }
    
    public Result AddTag(Tag tag)
    {
        if (_ticketTags.Any(tt => tt.TagId == tag.Id))
            return Result.Failure("Tag is already added to this ticket");

        var ticketTagResult = TicketTag.Create(Id, tag.Id);

        if (ticketTagResult.IsFailure)
            return Result.Failure(ticketTagResult.Error);

        _ticketTags.Add(ticketTagResult.Value);
        return Result.Success();
    }

    public Result RemoveTag(TagId  tagId)
    {
        var ticketTag = _ticketTags.FirstOrDefault(tt => tt.TagId == tagId);

        if (ticketTag is null)
        {
            return Result.Failure("This tag is not associated with the ticket");
        }

        _ticketTags.Remove(ticketTag);
        return Result.Success();
    }
    public Result EnsureCanReceiveMessages()
    {
        if (Status == TicketStatus.Completed)
            return Result.Failure("Ticket is closed");

        return Result.Success();
    }
}
