using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTickets.Domain;
using WebTickets.Domain.Modules;
using WebTickets.Domain.Roles;
using WebTickets.Domain.Users;

namespace WebTickets.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(Constants.HIGH_NAME_LENGTH);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(Constants.HIGH_EMAIL_LENGTH);

        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        builder.Property(u => u.RoleId)
            .HasConversion(
                id => id.Value,
                value => RoleId.Create(value))
            .IsRequired();
        
        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}