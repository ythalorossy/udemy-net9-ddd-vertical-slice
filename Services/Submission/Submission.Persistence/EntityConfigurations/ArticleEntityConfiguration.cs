using Blocks.EntityFrameworkCore;
using Blocks.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class ArticleEntityConfiguration : EntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd().HasColumnOrder(0);

        builder.Property(a => a.Title).IsRequired().HasMaxLength(256);
        builder.Property(a => a.Scope).IsRequired().HasMaxLength(2048);

        builder.Property(a => a.Stage).HasEnumConversion();
        builder.Property(a => a.Type).HasEnumConversion();

        builder.HasOne(a => a.Journal)
                .WithMany(j => j.Articles)
                .HasForeignKey(a => a.JournalId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Assets)
                .WithOne(e => e.Article)
                .HasForeignKey(e => e.ArticleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
    }
}
