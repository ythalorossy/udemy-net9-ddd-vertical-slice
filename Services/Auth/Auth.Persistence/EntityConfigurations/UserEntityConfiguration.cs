using Auth.Domain.Users;
using Blocks.Core;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal class UserEntityConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(MaxLength.C64);

        builder.Property(u => u.LasttName)
            .IsRequired()
            .HasMaxLength(MaxLength.C64);

        builder.Property(u => u.Gender)
            .IsRequired()
            .HasEnumConversion();

        builder.OwnsOne(u => u.Honorific, honorific =>
        {
            honorific.Property(h => h.Value)
                .HasMaxLength(MaxLength.C16)
                .HasColumnName(nameof(User.Honorific));

            honorific.WithOwner();  // required to avoid navigatoin issues
        });

        builder.OwnsOne(u => u.ProfessionalProfile, profile =>
        {
            profile.Property(p => p.Position)
                .HasMaxLength(MaxLength.C32)
                .HasColunmNameSameAsProperty();

            profile.Property(p => p.CompanyName)
                .HasMaxLength(MaxLength.C64)
                .HasColunmNameSameAsProperty();

            profile.Property(p => p.Affiliation)
                .HasMaxLength(MaxLength.C64)
                .HasColunmNameSameAsProperty();

            profile.WithOwner();  // required to avoid navigatoin issues
        });

        builder.Property(u => u.PictureUrl)
            .HasMaxLength(MaxLength.C2048);

        builder.HasMany(u => u.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
