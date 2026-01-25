namespace Submission.Domain.Entities;

public partial class Asset
{
    private Asset() { }

    internal static Asset Create(Article article, AssetTypeDefinition assetTypeDefinition)
    {
        return new Asset()
        {
            ArticleId = article.Id,
            Article = article,
            Name = AssetName.FromAssetType(assetTypeDefinition),
            Type = assetTypeDefinition.Name
        };
    }
}
