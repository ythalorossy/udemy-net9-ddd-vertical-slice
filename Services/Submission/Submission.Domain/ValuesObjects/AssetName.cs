using Blocks.Domain.ValueObjects;

namespace Submission.Domain.ValuesObjects;

public class AssetName : StringValueObject
{
    private AssetName(string value) => Value = value;

    public static AssetName FromAssetType(AssetTypeDefinition assetTypeDefinition)
        => new(assetTypeDefinition.Name.ToString());
}