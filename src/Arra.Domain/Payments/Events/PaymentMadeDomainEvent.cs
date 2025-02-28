using Arra.SharedKernel;

namespace Arra.Domain.Payments.Events;

public sealed record PaymentMadeDomainEvent(Guid Id) : IDomainEvent;

