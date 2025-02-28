using Arra.SharedKernel;

namespace Arra.Domain.Groups.Events;

public sealed record GroupMemberJoinedDomainEvent(Guid Id) : IDomainEvent;