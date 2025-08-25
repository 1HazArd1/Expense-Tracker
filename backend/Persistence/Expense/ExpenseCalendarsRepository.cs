using Expense.Tracker.Application.Interface.Persistence.Expense;
using Expense.Tracker.Domain.Expense;
using Expense.Tracker.Persistence.Shared;

namespace Expense.Tracker.Persistence.Expense
{
    public class ExpenseCalendarsRepository(IOLTPDatabaseContext database) : Repository<ExpenseCalendars>(database), IExpenseCalendarsRepository
    {
    }
}
