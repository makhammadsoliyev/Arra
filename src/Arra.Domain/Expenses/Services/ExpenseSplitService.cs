using Arra.SharedKernel;

namespace Arra.Domain.Expenses.Services;

public sealed class ExpenseSplitService
{
    private readonly Dictionary<SplitType, IExpenseSplitStrategy> splitStrategies;

    public ExpenseSplitService()
    {
        splitStrategies = new Dictionary<SplitType, IExpenseSplitStrategy>
        {
            {
                SplitType.Equal, new EqualSplitStrategy()
            },
            {
                SplitType.Percentege, new PercentageSplitStrategy()
            },
            {
                SplitType.Exact, new ExactSplitStrategy()
            }
        };
    }

    public List<ExpenseShare> SplitExpense(
        Guid expenseId,
        SplitType splitType,
        List<Guid> userIds,
        Money totalAmount,
        List<decimal> values = null)
    {
        if (!splitStrategies.ContainsKey(splitType))
            throw new NotImplementedException($"Split type {splitType} is not implemented.");

        return splitStrategies[splitType].Split(expenseId, userIds, totalAmount, values);
    }
}
