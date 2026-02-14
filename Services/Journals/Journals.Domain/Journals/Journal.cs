using Blocks.Redis;
using Redis.OM.Modeling;

namespace Journals.Domain.Journals;

[Document(StorageType = StorageType.Json)]
public class Journal : Entity
{
    [Indexed]
    public required string Name { get; set; }

    [Indexed]
    public required string Abbreviation { get; set; }

    public required string Description { get; set; }

    public required string ISSN { get; init; }

    [Indexed(JsonPath = "$.Name")]
    public int ChiefEditorId { get; set; }
}
