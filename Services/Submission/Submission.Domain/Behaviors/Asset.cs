using FIleStorage.Contracts;

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

    public string GenerateStorageFilePath(string fileName)
        => $"Articles/{ArticleId}/{Name}/{fileName}";

    public File CreateFile(UploadResponse uploadResponse, AssetTypeDefinition assetTypeDefinition)
    {

        File = File.CreateFile(uploadResponse, this, assetTypeDefinition);

        return File;
    }
}