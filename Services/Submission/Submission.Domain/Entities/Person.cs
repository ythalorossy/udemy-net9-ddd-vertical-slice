using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

public class Person : Entity
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string FullName => $"{FirstName} {LastName}";
    public string? Title { get; init; }
    public required EmailAddress EmailAddress { get; init; }
    public required string Affiliation { get; init; }
    public int? UserId { get; init; }

    public IReadOnlyCollection<ArticleActor> ArticleActors { get; private set; } = [];

    public string TypeDiscriminator { get; init; } = null!; // EF discriminator column
}