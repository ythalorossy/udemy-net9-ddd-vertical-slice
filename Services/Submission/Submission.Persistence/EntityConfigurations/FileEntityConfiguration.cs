using Blocks.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Submission.Persistence.EntityConfigurations;

public class FileEntityConfiguration
{
    public void Configure(ComplexPropertyBuilder<Domain.ValuesObjects.File> builder)
    {
        builder.Property(e => e.OriginalName)
            .HasMaxLength(MaxLength.C256)
            .HasComment("Original full file name including extension");

        builder.Property(e => e.FileServeId)
            .HasMaxLength(MaxLength.C64);

        builder.Property(e => e.Size)
            .HasComment("File size in kilobytes");

        builder.ComplexProperty(
            e => e.Extension, complexBuilder =>
            {
                complexBuilder.Property(e => e.Value)
                    .HasColumnName($"{complexBuilder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                    .HasMaxLength(MaxLength.C8);
            });

        builder.ComplexProperty(
            e => e.Name, complexBuilder =>
            {
                complexBuilder.Property(e => e.Value)
                    .HasColumnName($"{complexBuilder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                    .HasMaxLength(MaxLength.C64)
                    .HasComment("Final name of the file after renaming");
            });
    }
}
