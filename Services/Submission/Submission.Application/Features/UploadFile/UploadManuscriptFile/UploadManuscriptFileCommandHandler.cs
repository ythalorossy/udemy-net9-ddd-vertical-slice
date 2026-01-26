
using Blocks.EntityFramework;
using FIleStorage.Contracts;

namespace Submission.Application.Features.UploadFile.UploadManuscriptFile;

public class UploadManuscriptFileCommandHandler(
    ArticleRepository _articleRepository, AssetTypeDefinitionRepository _assetTypeRepository, IFileService _fileService)
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

        asset ??= article.CreateAsset(assetTypeDefinition);

        var filePath = asset.GenerateStorageFilePath(command.File.FileName);

        var uploadResponse = await _fileService.UploadFileAsync(
            filePath,
            command.File,
            overwrite: true,
            tags: new() {
                { "entity", nameof(Asset) },
                { "entityId", asset.Id.ToString() }
            }
        );

        try
        {
            asset.CreateFile(uploadResponse, assetTypeDefinition);

            await _articleRepository.SaveChangesAsync(cancellationToken);

        }
        catch (Exception)
        {
            await _fileService.TryDeleteFileAsync(uploadResponse.FileId);
            throw;
        }

        return new IdResponse(asset.Id);
    }
}