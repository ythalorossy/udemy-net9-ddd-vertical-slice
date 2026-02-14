using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFrameworkCore.EntityConfigurations;

public class EntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd().HasColumnOrder(0);
    }
}
