using FastEndpoints;
using FluentValidation;

namespace Auth.API.Features.CreateUser;

public class CreateUserCommandValidator : Validator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.UserRoles)
            .NotEmpty()
            .Must((command, roles) => AreUserRoleDatesValid(roles))
            .WithMessage("Invalid Roles");
    }

    public static bool AreUserRoleDatesValid(IReadOnlyList<UserRoleDto> roles)
    {
        return roles.All(role =>
            // StartDate must be today or in the future otherwise we might have a security concern
            (!role.StartDate.HasValue || role.StartDate.Value.Date >= DateTime.UtcNow.Date) &&

            // ExpiringDate must be after StartDate (or today if StartDate is null)
            (!role.ExpiringDate.HasValue || (role.StartDate ?? DateTime.UtcNow).Date < role.ExpiringDate.Value.Date)
        );
    }
}