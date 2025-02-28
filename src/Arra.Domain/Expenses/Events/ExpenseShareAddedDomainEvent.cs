using Arra.SharedKernel;

namespace Arra.Domain.Expenses.Events;

public sealed record ExpenseShareAddedDomainEvent(Guid Id) : IDomainEvent;
