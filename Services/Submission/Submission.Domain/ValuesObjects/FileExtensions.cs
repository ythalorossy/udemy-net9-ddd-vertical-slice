using Blocks.Core.Extensions;

namespace Submission.Domain.ValuesObjects;

public class FileExtensions
{
    public IReadOnlyList<string> Extensions { get; init; } = null!;

    public bool IsValidExtension(string extension)
        // NOTE: An empty list means all extensions are allowed
        => Extensions.IsEmpty() || Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
}