using Blocks.FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Submission.Application.Features.UploadFile;

public record UploadManuscriptFileCommand : ArticleCommand
{
    /// <summary>
    /// The Asset type of the file being uploaded.
    /// </summary>
    [Required]
    public AssetType AssetType { get; init; }

    /// <summary>
    /// The file to be uploaded.
    /// </summary>
    [Required]
    public IFormFile File { get; init; } = null!;

    public override ArticleActionType ActionType => ArticleActionType.Upload;
}

public class UploadManuscriptFileCommandValidator : ArticleCommandValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptFileCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNullWithMessage();

        // TODO: Validate File size, type, extension, etc.

        RuleFor(x => x.AssetType)
            .Must(IsAssetTypeAllowed)
            .WithMessage($"Asset types not allowed");
    }

    public IReadOnlyCollection<AssetType> AllowedAssetTypes = new HashSet<AssetType>
    {
        AssetType.Manuscript
    };

    public bool IsAssetTypeAllowed(AssetType assetType)
    {
        return AllowedAssetTypes.Contains(assetType);
    }
}