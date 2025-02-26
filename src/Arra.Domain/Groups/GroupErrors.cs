using Arra.SharedKernel;

namespace Arra.Domain.Groups;

public static class GroupErrors
{
    public static Error DuplicateMembership(Guid userId) => Error.Conflict(
        "Groups.DuplicateMembership",
        $"The user is already a member in this group with this id: {userId}");
}
