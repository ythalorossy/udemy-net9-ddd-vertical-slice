namespace Submission.Domain.Entities;

public partial class Journal
{
    public Article CreateArticle(string title, ArticleType type, string scope)
    {
        var article = new Article
        {
            Title = title,
            Type = type,
            Scope = scope,
            Journal = this,
            JournalId = this.Id,
            Stage = ArticleStage.Created
        };
        _articles.Add(article);
        // TODO: Add a domain event later
        return article;
    }
}
