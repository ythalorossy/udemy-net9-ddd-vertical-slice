using Auth.Domain.Persons;
using Auth.Domain.Roles;
using Auth.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence;

public class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : IdentityDbContext<User, Role, int>
{
    // The DbSet properties are inherated from IdentityDbContext

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        _ = options;

        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }
}