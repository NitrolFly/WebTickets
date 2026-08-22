using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTickets.Domain.Modules;

using File = WebTickets.Domain.Modules.File;

namespace WebTickets.Infrastructure.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable(nameof(File));

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
            .HasConversion(id => id.Value, value => FileId.Create(value));

        builder.Property(f => f.FileName).IsRequired().HasMaxLength(255);
        builder.Property(f => f.StorageKey).IsRequired().HasMaxLength(500);
        builder.Property(f => f.ContentType).IsRequired().HasMaxLength(100);
        builder.Property(f => f.SizeBytes).IsRequired();

        builder.Property(f => f.UploadedByUserId)
            .HasConversion(id => id.Value, value => UserId.Create(value))
            .IsRequired();

        builder.Property(f => f.TicketId)
            .HasConversion(
                id => id != null ? id.Value : (Guid?)null,
                value => value != null ? TicketId.Create(value.Value) : null);

        builder.HasOne(f => f.Ticket)
            .WithMany()
            .HasForeignKey(f => f.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(f => f.MessageId)
            .HasConversion(
                id => id != null ? id.Value : (Guid?)null,
                value => value != null ? MessageId.Create(value.Value) : null);

        builder.HasOne(f => f.Message)
            .WithMany()
            .HasForeignKey(f => f.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}