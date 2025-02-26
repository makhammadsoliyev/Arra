using Arra.SharedKernel;

namespace Arra.Domain.Expenses.Services;

public interface IExpenseSplitStrategy
{
    List<ExpenseShare> Split(Guid expenseId, List<Guid> userIds, Money totalAmount, List<decimal> values = null);
}
