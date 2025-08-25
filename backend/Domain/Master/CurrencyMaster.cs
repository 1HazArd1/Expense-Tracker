using Expense.Tracker.Domain.Common;

namespace Expense.Tracker.Domain.Master
{
    public class CurrencyMaster : IEntity
    {
        public required string CurrencyCode { get; set; }
        public required string CurrencyName { get; set; }
        public string? Symbol { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte Status { get; set; }
    }
}