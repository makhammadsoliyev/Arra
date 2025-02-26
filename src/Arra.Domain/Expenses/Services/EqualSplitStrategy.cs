using Arra.SharedKernel;

namespace Arra.Domain.Expenses.Services;

public sealed class EqualSplitStrategy : IExpenseSplitStrategy
{
    public List<ExpenseShare> Split(
        Guid expenseId,
        List<Guid> userIds,
        Money totalAmount,
        List<decimal> values = null)
    {
        var splitAmount = totalAmount / userIds.Count;

        return userIds
            .Select(userId => ExpenseShare.Create(expenseId, userId, splitAmount, SplitType.Equal, null))
            .ToList();
    }
}
