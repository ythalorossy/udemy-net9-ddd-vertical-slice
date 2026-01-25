using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(a => a.Discipline)
            .HasMaxLength(64)
            .HasComment("The author's main field of study or research (e.g., Biology, Computer Science");

        builder.Property(a => a.Degree)
            .HasMaxLength(64)
            .HasComment("The author's highest academic qualification (e.g., PhD in Mathematics, MSc Chemistry.");
    }
}
