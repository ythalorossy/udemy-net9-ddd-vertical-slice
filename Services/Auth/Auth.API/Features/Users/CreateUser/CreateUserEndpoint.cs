using Articles.Abstractions.Enums;
using Auth.Domain.Persons;
using Auth.Domain.Users.Events;
using Auth.Persistence;
using Auth.Persistence.Repositories;
using Blocks.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Features.Users.CreateUser;

[Authorize(Roles = Role.USERADMIN)]
[HttpPost("users")]
public class CreateUserEndpoint(
    PersonRepository _personRespository,
    AuthDbContext _dbContext,
    UserManager<User> _userManager)
    : Endpoint<CreateUserCommand, CreateUserResponse>
{
    public override async Task HandleAsync(CreateUserCommand command, CancellationToken ct)
    {
        var person = await _personRespository.GetbyUserEmailAsync(command.Email, ct);

        if (person?.User is not null)
        {
            throw new BadResquestException($"A user with the provided {command.Email} already exists.");
        }

        // Begin transation to persist multiple entities before commit the changes
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

        person ??= await CreateUserAsync(command, ct);

        var user = Domain.Users.User.Create(command);

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(" | ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
            throw new BadResquestException($"Unable to create user: {errorMessages}");
        }

        person.AssignUser(user);

        var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        await _personRespository.SaveChangesAsync(ct);

        await PublishAsync(new UserCreated(user, resetPasswordToken), Mode.WaitForAll, ct);

        await Send.OkAsync(new CreateUserResponse(command.Email, user.Id, resetPasswordToken), ct);
    }

    private async Task<Person> CreateUserAsync(CreateUserCommand command, CancellationToken ct)
    {
        var person = Person.Create(command);
        await _personRespository.AddAsync(person);
        await _personRespository.SaveChangesAsync(ct);

        return person;
    }
}
