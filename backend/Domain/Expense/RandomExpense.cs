using Expense.Tracker.Domain.Common;

namespace Expense.Tracker.Domain.Expense
{
    public class RandomExpense : IEntity
    {
        public long RandomExpenseId { get; set; }
        public long CalendarId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal ExpenseAmount { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public byte Status { get; set; }
    }
}