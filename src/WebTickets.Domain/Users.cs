namespace WebTickets.Domain;

public class Users
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    
    public SupportRole Role { get; set; }
    
    
    public enum SupportRole
    {
        Junior,
        Middle,
        Senior
    }
}
