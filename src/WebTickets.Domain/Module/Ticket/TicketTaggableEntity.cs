using WebTickets.Domain.Modules;
namespace WebTickets.Domain.Shared;
using CSharpFunctionalExtensions;

public abstract class TicketTaggableEntity<TId> : Shared.Entity<TId>
{
    protected TicketTaggableEntity(TId id) : base(id) { }
    protected  readonly List<TicketTag> _ticketTags = new();
    public virtual IReadOnlyCollection<TicketTag> TicketTags => _ticketTags.AsReadOnly();
    
   
}
