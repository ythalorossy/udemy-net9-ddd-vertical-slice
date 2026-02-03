using Articles.Abstractions.Enums;
using Auth.Domain.Users.Events;
using Blocks.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Features.CreateUser;

[Authorize(Roles = Role.USERADMIN)]
[HttpPost("users")]
public class CreateUserEndpoint(UserManager<User> _userManager)
    : Endpoint<CreateUserCommand, CreateUserResponse>
{
    public override async Task HandleAsync(CreateUserCommand command, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is not null)
        {
            throw new BadResquestException($"A user with the provided {command.Email} already exists.");
        }

        user = Domain.Users.User.Create(command);

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(" | ", result.Errors.Select(e => e.Description));
            throw new BadResquestException($"Failed to create user: {errorMessages}");
        }

        var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        await PublishAsync(new UserCreated(user, resetPasswordToken), Mode.WaitForAll, ct);

        await Send.OkAsync(new CreateUserResponse(command.Email, user.Id, resetPasswordToken), ct);
    }
}
