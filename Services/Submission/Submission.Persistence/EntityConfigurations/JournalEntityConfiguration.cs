using Blocks.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class JournalEntityConfiguration : EntityConfiguration<Journal>
{
    public override void Configure(EntityTypeBuilder<Journal> builder)
    {
        base.Configure(builder);

        builder.Property(j => j.Name).IsRequired().HasMaxLength(64);
        builder.Property(j => j.Abreviation).IsRequired().HasMaxLength(8);
    }
}