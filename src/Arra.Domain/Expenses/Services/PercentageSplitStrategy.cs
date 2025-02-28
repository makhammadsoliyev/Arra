using Arra.SharedKernel;

namespace Arra.Domain.Expenses.Services;

public sealed class PercentageSplitStrategy : IExpenseSplitStrategy
{
    public List<ExpenseShare> Split(
        Guid expenseId,
        List<Guid> userIds,
        Money totalAmount,
        List<decimal> values = null)
    {
        if (values == null || values.Sum() != 100)
            throw new ArgumentException("Percentages must sum to 100%");

        return userIds.Select((userId, index) =>
        {
            var percentage = Percentage.Create(values[index]);
            return ExpenseShare.Create(
                expenseId,
                userId,
                totalAmount / percentage.Value,
                SplitType.Percentege,
                percentage);
        }).ToList();
    }
}
