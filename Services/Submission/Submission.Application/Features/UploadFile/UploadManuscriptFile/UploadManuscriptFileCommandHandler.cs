
using Blocks.EntityFramework;

namespace Submission.Application.Features.UploadFile.UploadManuscriptFile;

public class UploadManuscriptFileCommandHandler(
    ArticleRepository _articleRepository, AssetTypeDefinitionRepository _assetTypeRepository)
    : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
{
    public async Task<IdResponse> Handle(UploadManuscriptFileCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        var assetTypeDefinition = _assetTypeRepository.GetById(command.AssetType);

        Asset? asset = null;

        if (!assetTypeDefinition.AllowsMultipleAssets)
        {
            asset = article.Assets.SingleOrDefault(e => e.Type == assetTypeDefinition.Id);
        }

        if (asset is null)
        {
            asset = article.CreateAsset(assetTypeDefinition);
        }

        // TODO : Upload the file to the file storage

        await _articleRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(asset.Id);
    }
}