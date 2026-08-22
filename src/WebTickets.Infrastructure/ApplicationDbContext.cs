using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTickets.Domain.Modules;
using WebTickets.Domain.Roles;
using WebTickets.Domain.Users;

namespace WebTickets.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TicketTag> TicketTags => Set<TicketTag>();
    public DbSet<Domain.Modules.File> Files => Set<Domain.Modules.File>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Зарегистрировать конвертер СРАЗУ (до ApplyConfigurationsFromAssembly)
        var ticketIdConverter = new ValueConverter<TicketId, Guid>(
            id => id.Value,
            guid => TicketId.Create(guid)
        );

        // Автоматически применить конвертер ко всем свойствам типа TicketId
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            if (clrType == null) continue;

            foreach (var prop in clrType.GetProperties().Where(p => p.PropertyType == typeof(TicketId)))
            {
                modelBuilder.Entity(clrType).Property(prop.Name).HasConversion(ticketIdConverter);
            }
        }

        // Применяем конфигурации из сборки
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}