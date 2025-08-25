using Expense.Tracker.Domain.Common;

namespace Expense.Tracker.Domain.Expense
{
    public class ManipulatedRecurringExpense : IEntity
    {
        public long ManipulatedEntryId { get; set; }
        public long CalendarId { get; set; }
        public DateTime ManipulatedDate { get; set; }
        public decimal? RecurringQuantity { get; set; }
        public bool IsOccurrenceSkipped { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public byte Status { get; set; }
    }
}