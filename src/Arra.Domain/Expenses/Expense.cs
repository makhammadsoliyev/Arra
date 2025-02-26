using Arra.Domain.Expenses.Events;
using Arra.Domain.Groups;
using Arra.Domain.Users;
using Arra.SharedKernel;

namespace Arra.Domain.Expenses;

public sealed class Expense : AggregateRoot
{
    private readonly List<ExpenseShare> shares;

    private Expense(
        Guid id,
        Guid groupId,
        Guid paidById,
        Money amount,
        Description description,
        DateTime createdOnUtc)
    {
        Id = id;
        GroupId = groupId;
        PaidById = paidById;
        Amount = amount;
        Description = description;
        CreatedOnUtc = createdOnUtc;
        shares = [];
    }

    public Guid GroupId { get; private set; }

    public Group Group { get; private set; }

    public Guid PaidById { get; private set; }

    public User PaidBy { get; private set; }

    public Money Amount { get; private set; }

    public Description Description { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public List<ExpenseShare> Shares => [.. shares];

    public static Expense Create(
        Guid groupId,
        Guid paidById,
        Money amount,
        Description description,
        DateTime createdOnUtc)
    {
        return new Expense(
            Guid.NewGuid(),
            groupId,
            paidById,
            amount,
            description,
            createdOnUtc);
    }

    public Result AddShare(
        Guid userId,
        Money amountOwned,
        SplitType splitType,
        Percentege percentege)
    {
        if (shares.Any(share => share.UserId == userId))
            return Result.Failure(ExpenseErrors.DuplicateShare(userId));

        var newShare = ExpenseShare.Create(
            Id,
            userId,
            amountOwned,
            splitType,
            percentege);
        shares.Add(newShare);

        RaiseDomainEvent(new ExpenseShareAddedDomainEvent(newShare.Id));

        return Result.Success();
    }
}
