using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTickets.Domain;
using WebTickets.Domain.Modules;
using WebTickets.Domain.Roles;

namespace WebTickets.Infrastructure.Configurations;

public class RoleConfiguration: IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value,
                value => RoleId.Create(value));

        builder.Property(r => r.RoleName)
            .IsRequired()
            .HasMaxLength(Constants.HIGH_NAME_LENGTH);

        builder.HasIndex(r => r.RoleName)
            .IsUnique();

        builder.Metadata
            .FindNavigation(nameof(Role.Permissions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.PrimitiveCollection<List<Role.Permission>>("_permissions")
            .HasColumnName("Permissions")
            .ElementType(e => e.HasConversion(typeof(string)));
    }
}