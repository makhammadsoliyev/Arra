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
        Guid paidByUserId,
        Money amount,
        Description description,
        DateTime createdOnUtc) : base(id)
    {
        GroupId = groupId;
        PaidByUserId = paidByUserId;
        Amount = amount;
        Description = description;
        CreatedOnUtc = createdOnUtc;
        shares = [];
    }

    public Guid GroupId { get; private set; }

    public Group Group { get; private set; }

    public Guid PaidByUserId { get; private set; }

    public User PaidByUser { get; private set; }

    public Money Amount { get; private set; }

    public Description Description { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public List<ExpenseShare> Shares => [.. shares];

    public static Expense Create(
        Guid groupId,
        Guid paidByUserId,
        Money amount,
        Description description,
        DateTime createdOnUtc)
    {
        var newExpense = new Expense(
            Guid.NewGuid(),
            groupId,
            paidByUserId,
            amount,
            description,
            createdOnUtc);

        return newExpense;
    }

    public Result AddShare(
        Guid userId,
        Money amountOwned,
        SplitType splitType,
        Percentage percentege)
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

        return Result.Success();
    }
}
