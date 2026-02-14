using FastEndpoints;
using FluentValidation;

namespace Journals.API.Features.Create;

public record CreateJournalCommand(string Name, string Abbreviation, string Description, string ISSN, int ChiefEditorId)
{
}

public class CreateJournalCommandvalidator : Validator<CreateJournalCommand>
{
    public CreateJournalCommandvalidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Abbreviation).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.ISSN).NotEmpty().Matches(@"\d{4}-\d{3}[\dX]").WithMessage("Invalid ISSN format");
        RuleFor(x => x.ChiefEditorId).GreaterThan(0);
    }
}