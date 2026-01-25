using Blocks.Core;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class AssetTypeDefinitionEntityConfiguration : IEntityTypeConfiguration<AssetTypeDefinition>
{
    public void Configure(EntityTypeBuilder<AssetTypeDefinition> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.Name)
            .HasEnumConversion()
            .HasMaxLength(MaxLength.C64)
            .IsRequired()
            .HasColumnOrder(1);

        builder.Property(e => e.MaxFileSizeInMB)
            .HasDefaultValue(5); // Default to 5 MB

        builder.Property(e => e.DefaultFileExtension)
            .HasMaxLength(MaxLength.C8)
            .HasDefaultValue("pdf")
            .IsRequired();

        builder.ComplexProperty(
            e => e.AllowedFileExtensions, builder =>
            {
                var converter = BuilderExtensions.BuildJsonCollectionConvertor<string>();

                builder.Property(equals => equals.Extensions)
                    .HasConversion(converter)
                    .HasColumnName(builder.Metadata.PropertyInfo!.Name)
                    .IsRequired();
            });
    }
}
