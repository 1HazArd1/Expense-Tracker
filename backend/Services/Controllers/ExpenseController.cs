using Expense.Tracker.Application.Expense;
using Expense.Tracker.Domain.Expense;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expense.Tracker.Services.Controllers
{
    [Route("expense")]
    public class ExpenseController(ISender sender) : ApiControllerBase
    {
        [HttpGet("calendar")]
        public async Task<ExpenseCalendars?> GetCalendar([FromQuery] long calendarId)
        {
            return await sender.Send(new GetExpenseDetailsQuery(calendarId));
        }

    }
}
