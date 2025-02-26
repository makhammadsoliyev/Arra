using Arra.SharedKernel;

namespace Arra.Domain.Expenses;

public static class ExpenseErrors
{
    public static Error DuplicateShare(Guid userId) => Error.Conflict(
        "Expenses.DuplicateExpenseShare",
        $"The user already has a share in this expense: {userId}");
}
