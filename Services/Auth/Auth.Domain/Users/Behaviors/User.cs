using Auth.Domain.Users.ValueObjects;
using Blocks.Core.Extensions;

namespace Auth.Domain.Users;

public partial class User
{
    public static User Create(IUserCreationInfo userInfo)
    {
        if (userInfo.UserRoles.IsNullOrEmpty())
        {
            throw new ArgumentException("User must have at least one role assigned.", nameof(userInfo.UserRoles));
        }

        var user = new User
        {
            UserName = userInfo.Email,
            Email = userInfo.Email,
            FirstName = userInfo.FirstName,
            LasttName = userInfo.LastName,
            Gender = userInfo.Gender,
            PhoneNumber = userInfo.PhoneNumber,
            PictureUrl = userInfo.PictureUrl,
            Honorific = HonorificTitle.FromEnum(userInfo.Honorific),
            ProfessionalProfile = ProfessionalProfile.Create(
                affiliation: userInfo.Affiliation,
                companyName: userInfo.CompanyName,
                position: userInfo.Position),
            _userRoles = [.. userInfo.UserRoles.Select(ur => UserRole.Create(ur))],
        };

        // TODO: Add domain events if needed

        return user;
    }
}