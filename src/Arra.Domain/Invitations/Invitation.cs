using Arra.Domain.Users;
using Arra.SharedKernel;
using System.Text.RegularExpressions;

namespace Arra.Domain.Invitations;

public sealed class Invitation : Entity
{
    private Invitation(
        Guid id,
        Guid groupId,
        Guid invitedByUserId,
        Email invitedEmail,
        InvitationStatus status,
        DateTime invitedOnUtc,
        DateTime? acceptedOnUtc,
        DateTime? rejectedOnUtc) : base(id)
    {
        GroupId = groupId;
        InvitedByUserId = invitedByUserId;
        InvitedEmail = invitedEmail;
        Status = status;
        InvitedOnUtc = invitedOnUtc;
        AcceptedOnUtc = acceptedOnUtc;
        RejectedOnUtc = rejectedOnUtc;
    }
    public Guid GroupId { get; private set; }

    public Group Group { get; private set; }

    public Guid InvitedByUserId { get; private set; }

    public User InvitedByUser { get; private set; }

    public Email InvitedEmail { get; private set; }

    public InvitationStatus Status { get; private set; }

    public DateTime InvitedOnUtc { get; private set; }

    public DateTime? AcceptedOnUtc { get; private set; }

    public DateTime? RejectedOnUtc { get; private set; }

    public static Invitation Create(
        Guid groupId,
        Guid invitedByUserId,
        Email invitedEmail,
        DateTime invitedOnUtc)
    {
        var newInvitation = new Invitation(
            Guid.NewGuid(),
            groupId,
            invitedByUserId,
            invitedEmail,
            InvitationStatus.Invited,
            invitedOnUtc,
            null,
            null);

        return newInvitation;
    }

    public Result Accept(DateTime acceptedOnUtc)
    {
        if (Status != InvitationStatus.Invited)
            return Result.Failure(InvitationErrors.InvalidStatusChange());

        AcceptedOnUtc = acceptedOnUtc;
        Status = InvitationStatus.Accepted;

        return Result.Success();
    }

    public Result Reject(DateTime rejectedOnUtc)
    {
        if (Status != InvitationStatus.Invited)
            return Result.Failure(InvitationErrors.InvalidStatusChange());

        RejectedOnUtc = rejectedOnUtc;
        Status = InvitationStatus.Rejected;

        return Result.Success();
    }
}
