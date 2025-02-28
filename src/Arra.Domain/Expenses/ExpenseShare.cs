using Arra.Domain.Expenses.Events;
using Arra.SharedKernel;

namespace Arra.Domain.Expenses;

public sealed class ExpenseShare : Entity
{
    private ExpenseShare(
        Guid id,
        Guid expenseId,
        Guid userId,
        Money amountOwned,
        SplitType splitType,
        Percentage percentege) : base(id)
    {
        ExpenseId = expenseId;
        UserId = userId;
        AmountOwned = amountOwned;
        SplitType = splitType;
        Percentege = percentege;
    }

    public Guid ExpenseId { get; private set; }

    public Guid UserId { get; private set; }

    public Money AmountOwned { get; private set; }

    public SplitType SplitType { get; private set; }

    public Percentage Percentege { get; private set; }

    public static ExpenseShare Create(
        Guid expenseId,
        Guid userId,
        Money amountOwned,
        SplitType splitType,
        Percentage percentege)
    {
        var newShare = new ExpenseShare(
            Guid.NewGuid(),
            expenseId,
            userId,
            amountOwned,
            splitType,
            percentege);

        newShare.RaiseDomainEvent(new ExpenseShareAddedDomainEvent(newShare.Id));

        return newShare;
    }
}
