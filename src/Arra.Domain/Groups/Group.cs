using Arra.SharedKernel;

namespace Arra.Domain.Groups;

public sealed class Group : AggregateRoot
{
    private readonly List<GroupMember> members;

    private Group(
        Guid id,
        GroupName name,
        Guid ownerId,
        DateTime createdOnUtc) : base(id)
    {
        Name = name;
        CreatedOnUtc = createdOnUtc;
        members = [];

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

        return Result.Success();
    }
}
