using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTickets.Domain;
using WebTickets.Domain.Modules;

namespace WebTickets.Infrastructure.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable(nameof(Ticket));

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => TicketId.Create(value));

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(Constants.MIN_TEXT_LENGTH);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(Constants.HIGH_TEXT_LENGTH);
    }
}