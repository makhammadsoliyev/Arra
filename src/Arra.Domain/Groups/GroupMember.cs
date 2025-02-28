using Arra.Domain.Groups.Events;
using Arra.Domain.Users;
using Arra.SharedKernel;

namespace Arra.Domain.Groups;

public sealed class GroupMember : Entity
{
    private GroupMember(
        Guid id,
        Guid groupId,
        Guid userId,
        GroupRole role,
        DateTime joinedOnUtc) : base(id)
    {
        GroupId = groupId;
        UserId = userId;
        Role = role;
        JoinedOnUtc = joinedOnUtc;
    }

    public Guid GroupId { get; private set; }

    public Group Group { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public GroupRole Role { get; private set; }

    public DateTime JoinedOnUtc { get; private set; }

    public static GroupMember Create(
        Guid groupId,
        Guid userId,
        GroupRole role,
        DateTime joinedOnUtc)
    {
        var newMember = new GroupMember(
            Guid.NewGuid(),
            groupId,
            userId,
            role,
            joinedOnUtc);
        newMember.RaiseDomainEvent(new GroupMemberJoinedDomainEvent(newMember.Id));

        return newMember;
    }
}
