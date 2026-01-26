using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValuesObjects;

public class FileName : StringValueObject
{
    private FileName(string value) => Value = value;

    public static FileName FromAsset(Asset asset, FileExtension extension)
    {
        var assetName = asset.Name.Value;

        var value = $"{assetName}.{extension.Value}";

        return new FileName(value);
    }
}
