using Arra.Domain.Groups.Events;
using Arra.SharedKernel;

namespace Arra.Domain.Groups;

public sealed class Group : AggregateRoot
{
    private readonly List<GroupMember> members = [];

    private Group(
        Guid id,
        GroupName name,
        Guid ownerId,
        DateTime createdOnUtc)
    {
        Id = id;
        Name = name;
        CreatedOnUtc = createdOnUtc;

        AddMember(ownerId, GroupRole.Owner, createdOnUtc);
    }

    public GroupName Name { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public List<GroupMember> Members => [.. members];

    public static Group Create(GroupName name, Guid ownerId, DateTime createdOnUtc)
    {
        return new Group(Guid.NewGuid(), name, ownerId, createdOnUtc);
    }

    public Result AddMember(Guid userId, GroupRole role, DateTime joinedOnUtc)
    {
        if (members.Any(memeber => memeber.UserId == userId))
            return Result.Failure(GroupErrors.DuplicateMembership(userId));

        var newMember = GroupMember.Create(Id, userId, role, joinedOnUtc);
        members.Add(newMember);

        RaiseDomainEvent(new GroupMemberAddedDomainEvent(this.Id, userId));

        return Result.Success();
    }
}
