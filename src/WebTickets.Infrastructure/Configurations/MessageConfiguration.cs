using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTickets.Domain;
using WebTickets.Domain.Modules;

namespace WebTickets.Infrastructure.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable(nameof(Message));

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => MessageId.Create(value));

        builder.Property(m => m.TicketId)
            .HasConversion(
                id => id.Value,
                value => TicketId.Create(value))
            .IsRequired();

        builder.HasOne(m => m.Ticket)
            .WithMany()
            .HasForeignKey(m => m.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(m => m.AuthorId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value))
            .IsRequired();

        builder.HasOne(m => m.Author)
            .WithMany()
            .HasForeignKey(m => m.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(m => m.Text)
            .IsRequired()
            .HasMaxLength(Constants.HIGH_TEXT_LENGTH);

        builder.Property(m => m.SentAt)
            .IsRequired();

        builder.HasIndex(m => new { m.TicketId, m.SentAt });
    }
}