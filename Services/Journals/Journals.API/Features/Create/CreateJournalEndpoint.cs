using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Blocks.Exceptions;
using Blocks.Redis;
using FastEndpoints;
using Journals.Domain.Journals;
using Journals.Domain.Journals.Events;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace Journals.API.Features.Create;

[Authorize(Roles = Role.EOF)]
[HttpPost("journals")]
[Tags("Journals")]
public class CreateJournalEndpoint(
    Repository<Journal> _journalrepository, Repository<Editor> _editorRepository)
    : Endpoint<CreateJournalCommand, IdResponse>
{
    public override async Task HandleAsync(CreateJournalCommand command, CancellationToken ct)
    {

        if (_journalrepository.Collection.Any(j => j.Abbreviation == command.Abbreviation || j.Name == command.Name))
        {
            throw new BadResquestException($"Journal with the same name or abbreviation already exists");
        }

        if (!_editorRepository.Collection.Any(e => e.Id == command.ChiefEditorId))
        {
            // TODO: Get the editor from Auth Service
        }

        var journal = command.Adapt<Journal>();

        await _journalrepository.AddAsync(journal);

        await _journalrepository.SaveAllAsync();

        // TODO: Publish JournalCreatedEvent to Event Bus
        await PublishAsync(new JournalCreated(journal), cancellation: ct);

        await Send.OkAsync(new IdResponse(journal.Id), cancellation: ct);
    }
}
