using Auth.Domain.Roles;
using Blocks.Core;
using Blocks.EntityFrameworkCore;
using Blocks.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal class RoleEntityConfiguration : EntityConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.Property(r => r.Type)
            .HasEnumConversion()
            .IsRequired();

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(MaxLength.C256);
    }
}