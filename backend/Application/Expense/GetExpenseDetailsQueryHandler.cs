using Expense.Tracker.Application.Interface.Persistence.Expense;
using Expense.Tracker.Domain.Expense;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracker.Application.Expense
{
    public record GetExpenseDetailsQuery(long ExpenseId) : IRequest<ExpenseCalendars?>;
    public class GetExpenseDetailsQueryHandler(IExpenseCalendarsRepository expenseCalendarsRepository) : IRequestHandler<GetExpenseDetailsQuery, ExpenseCalendars?>
    {
        public async Task<ExpenseCalendars?> Handle(GetExpenseDetailsQuery request, CancellationToken cancellationToken)
        {
            return await expenseCalendarsRepository.GetAllAsNoTracking()
                .Where(e => e.CalendarId == request.ExpenseId)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
