using Arra.SharedKernel;

namespace Arra.Domain.Invitations;

public class InvitationErrors
{
    public static Error InvalidStatusChange() => Error.Failure(
        "Invitation.InvalidStatusChange",
        "Invalid status change");
}