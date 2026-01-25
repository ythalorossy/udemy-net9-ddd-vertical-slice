using Blocks.Domain.Entities;

namespace Submission.Domain.Entities;

public partial class Journal : Entity
{
    public required string Name { get; set; }
    public required string Abreviation { get; set; }

    private readonly IList<Article> _articles = [];
    public IReadOnlyList<Article> Articles => _articles.AsReadOnly();
}
