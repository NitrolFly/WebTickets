using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTickets.Domain.Modules;

namespace WebTickets.Infrastructure.Configurations;

public class TicketTagConfiguration : IEntityTypeConfiguration<TicketTag>
{
    public void Configure(EntityTypeBuilder<TicketTag> builder)
    {
        builder.ToTable(nameof(TicketTag));

        builder.HasKey(tt => tt.Id);
        builder.Property(tt => tt.Id)
            .HasConversion(
                id => id.Value,
                value => TicketTagId.Create(value));

        builder.Property(tt => tt.TicketId)
            .HasConversion(
                id => id.Value,
                value => TicketId.Create(value));

        builder.Property(tt => tt.TagId)
            .HasConversion(
                id => id.Value,
                value => TagId.Create(value));

        builder.HasOne(tt => tt.Ticket)
            .WithMany(tt => tt.TicketTags)
            .HasForeignKey(tt => tt.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}