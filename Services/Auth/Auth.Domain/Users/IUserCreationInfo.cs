using Articles.Abstractions;

namespace Auth.Domain.Users;

public interface IUserCreationInfo : IPersonCreationInfo
{
    string? PhoneNumber { get; }
    IReadOnlyList<IUserRole> UserRoles { get; }
}