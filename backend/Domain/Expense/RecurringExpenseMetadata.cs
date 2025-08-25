using Expense.Tracker.Domain.Common;

namespace Expense.Tracker.Domain.Expense
{
    public class RecurringExpenseMetadata : IEntity
    {
        public long ExpenseMetadataId { get; set; }
        public long CalendarId { get; set; }
        public byte ExpenseFrequency { get; set; }
        public DateTime RecurringStartDate { get; set; }
        public DateTime? RecurringEndDate { get; set; }
        public byte UnitId { get; set; }
        public decimal? CostPerUnit { get; set; }
        public decimal? RecurringQuantity { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public byte Status { get; set; }
    }
}