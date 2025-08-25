using Expense.Tracker.Domain.Common;

namespace Expense.Tracker.Domain.Master
{
    public class UnitOfMeasure : IEntity
    {
        public byte UnitId { get; set; }
        public required string UnitName { get; set; }
        public string? UnitSymbol { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}