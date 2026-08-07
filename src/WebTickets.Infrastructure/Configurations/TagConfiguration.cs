using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTickets.Domain;
using WebTickets.Domain.Modules;

namespace WebTickets.Infrastructure.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => TagId.Create(value));

        builder.Property(n => n.TagName)
            .IsRequired()
            .HasMaxLength(Constants.MIN_TEXT_LENGTH);
        builder.HasIndex(t => t.TagName).IsUnique();

        builder.HasMany(t => t.TicketTags)
            .WithOne(tt => tt.Tag)
            .HasForeignKey(tt => tt.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(t => t.TicketTags)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
    }
}