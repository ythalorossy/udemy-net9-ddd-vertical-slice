using Blocks.Core;
using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValuesObjects;

public class FileExtension : StringValueObject
{
    private FileExtension(string value) => Value = value;

    public static FileExtension FromFileName(string fileName, AssetTypeDefinition assetTypeDefinition)
    {
        var extension = Path.GetExtension(fileName)[1..];

        Guard.ThrowIfNullOrWhiteSpace(extension);

        Guard.ThrowIfNotEqual(assetTypeDefinition.AllowedFileExtensions.IsValidExtension(extension), true);

        return new FileExtension(extension);
    }
}
