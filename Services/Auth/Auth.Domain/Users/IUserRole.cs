using Articles.Abstractions.Enums;

namespace Auth.Domain.Users;

public interface IUserRole
{
    DateTime? ExpiringDate { get; }
    DateTime? StartDate { get; }
    UserRoleType Type { get; }
}