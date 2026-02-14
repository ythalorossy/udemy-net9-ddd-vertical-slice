using Auth.Application;
using Auth.Persistence.Repositories;
using Blocks.AspNetCore;
using Blocks.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Features.Users.Login;

[AllowAnonymous]
[HttpPost("login")]
public class LoginEndpoint(
    PersonRepository _personRepository,
    UserManager<User> _userManager, SignInManager<User> _signInManager, TokenFactory _tokenFactory)
    : Endpoint<LoginCommand, LoginResponse>
{
    public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
    {
        var person = Guard.NotFound(await _personRepository.GetbyUserEmailAsync(command.Email, ct));

        var user = Guard.NotFound(person.User);

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, command.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new BadResquestException($"Invalid credentials for {command.Email}");
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var jwtToken = _tokenFactory.GenerateJWTToken(
            user.Id.ToString(),
            user.Person.FullName,
            command.Email,
            userRoles,
            additionalClaims: []);

        var refreshToken = _tokenFactory.GenerateRefreshToken(HttpContext.GetClientIpAddress());

        user.AddRefreshToken(refreshToken);

        await Send.OkAsync(new LoginResponse(command.Email, jwtToken, refreshToken.Token), ct);
    }
}
