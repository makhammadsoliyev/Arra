using Arra.SharedKernel;

namespace Arra.Domain.Groups.Events;

public sealed record GroupMemberAddedDomainEvent(Guid GroupId, Guid UserId)
    : IDomainEvent;
