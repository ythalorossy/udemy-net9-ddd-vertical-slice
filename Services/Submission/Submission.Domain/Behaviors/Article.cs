using Blocks.Domain;

namespace Submission.Domain.Entities;

public partial class Article
{
    public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor)
    {
        var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;

        if (Actors.Exists(a => a.PersonId == author.Id && a.Role == role))
        {
            throw new DomainException($"Author {author.EmailAddress} is already assigned to the article");
        }

        Actors.Add(new ArticleAuthor()
        {
            ContributionAreas = contributionAreas,
            Person = author,
            Role = role,
        });

        // TODO: Create Domain Event
    }

    public Asset CreateAsset(AssetTypeDefinition assetTypeDefinition)
    {
        var assetCount = _assets
            .Where(a => a.Type == assetTypeDefinition.Id)
            .Count();

        if (assetTypeDefinition.MaxAssetCount > assetCount - 1)
        {
            throw new DomainException($"Cannot add more assets of type {assetTypeDefinition.Name} to article {Id}");
        }

        var asset = Asset.Create(this, assetTypeDefinition);

        _assets.Add(asset);

        return asset;
    }
}
