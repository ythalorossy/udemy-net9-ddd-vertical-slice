using Blocks.FluentValidation;

namespace Submission.Application.Features.CreateArticle;

public record CreateArticleCommand(int JornalId, string Title, string Scope, ArticleType Type)
    : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.Create;
}

public class CreateArticleCommandValidator : ArticleCommandValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmptyWithMessage(nameof(CreateArticleCommand.Title));

        RuleFor(x => x.Scope)
            .NotEmptyWithMessage(nameof(CreateArticleCommand.Scope));

        RuleFor(x => x.JornalId)
              .GreaterThan(0)
              .WithMessageForInvalidId(nameof(CreateArticleCommand.JornalId));
    }
}