using CSharpFunctionalExtensions;
using WebTickets.Domain.Shared;

namespace WebTickets.Domain.Modules;

public class Tag : TicketTaggableEntity<TagId>
{
    private Tag(TagId tagId, string tagName) : base(tagId)
    {
        TagName = tagName;
    }
    
    public string TagName { get; private set; } = null!;
    
    public static Result<Tag> Create(string tagName)
    {
        if (string.IsNullOrWhiteSpace(tagName))
            return Result.Failure<Tag>("Tag name cannot be empty");
        
        if (tagName.Length > Constants.MIN_TEXT_LENGTH)
            return Result.Failure<Tag>($"Tag name cannot exceed {Constants.MIN_TEXT_LENGTH} characters");
        
        var tagId = TagId.NewTagId();
        return Result.Success(new Tag(tagId, tagName.Trim()));
    }
}