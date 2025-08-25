using Expense.Tracker.Domain.Common;

namespace Expense.Tracker.Domain.Expense
{
    public class ExpenseCalendars : IEntity
    {
        public long CalendarId { get; set; }
        public long UserId { get; set; }
        public required string ExpenseName { get; set; }
        public byte ExpenseType { get; set; }
        public required string CurrencyCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public byte Status { get; set; }
    }
}
