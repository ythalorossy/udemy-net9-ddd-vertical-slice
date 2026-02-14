using Auth.Domain.Persons;
using Blocks.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories;

public class PersonRepository(AuthDbContext authDbContext)
    : Repository<AuthDbContext, Person>(authDbContext)
{
    public async Task<Person?> GetbyUserIdAsync(int userId, CancellationToken cancellationToken)
        => await Query()
            .SingleOrDefaultAsync(e => e.UserId == userId, cancellationToken);

    public async Task<Person?> GetbyUserEmailAsync(string email, CancellationToken cancellationToken)
        => await Query()
            .SingleOrDefaultAsync(
                e => e.Email.NormalizedEmail.Equals(email, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

    protected override IQueryable<Person> Query()
    {
        return base.Query().Include(p => p.User);
    }
}
