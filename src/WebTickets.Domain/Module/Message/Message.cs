using CSharpFunctionalExtensions;
using WebTickets.Domain.Users;

namespace WebTickets.Domain.Modules;

public class Message : Shared.Entity<MessageId>
{
    private Message(MessageId id) : base(id) { }

    private Message(MessageId id, TicketId ticketId, UserId authorId, string text) : base(id)
    {
        TicketId = ticketId;
        AuthorId = authorId;
        Text = text;
        SentAt = DateTime.UtcNow;
    }

    public TicketId TicketId { get; private set; } = default!;
    public virtual Ticket Ticket { get; private set; } = null!;
    
    public UserId AuthorId { get; private set; } = default!;
    public virtual User Author { get; private set; } = null!;

    public string Text { get; private set; } = null!;
    public DateTime SentAt { get; private set; }
    
    
    public static Result<Message> Create(TicketId ticketId, UserId authorId, string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Result.Failure<Message>("Message text can not be empty");

        var message = new Message(MessageId.NewMessageId(), ticketId, authorId, text);
        return Result.Success(message);
    }
}

