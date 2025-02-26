using Arra.SharedKernel;

namespace Arra.Domain.Expenses.Services;

public sealed class ExactSplitStrategy : IExpenseSplitStrategy
{
    public List<ExpenseShare> Split(
        Guid expenseId,
        List<Guid> userIds,
        Money totalAmount,
        List<decimal> values = null)
    {
        if (values == null || values.Count != userIds.Count || values.Sum() != totalAmount.Amount)
            throw new ArgumentException("Exact amounts must sum to total expense");

        return userIds
            .Select((userId, index)
                => ExpenseShare.Create(
                    expenseId,
                    userId,
                    Money.Create(values[index], totalAmount.Currency),
                    SplitType.Exact,
                    null))
            .ToList();
    }
}
