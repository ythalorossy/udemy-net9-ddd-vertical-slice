using Blocks.EntityFrameworkCore;


namespace Submission.Application.Features.CreateAndAssignAuthor;

public class CreateAndAssignAuthorCommandHandler(ArticleRepository _articleRepository)
    : IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        Author? author = null;
        if (command.UserId is null) // Author is not User
                                    // author = command.Adapt<Author>();
            author = Author.Create(command.Email!, command.FirstName!, command.LastName!, command.Title, command.Affiliation!);
        else { }    // TODO: Author is an User

        article.AssignAuthor(author!, command.ContributionAreas, command.IsCorrespondingAuthor);

        await _articleRepository.SaveChangesAsync(cancellationToken);

        return new IdResponse(article.Id);
    }
}
