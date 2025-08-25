using Expense.Tracker.Domain.Expense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense.Tracker.Persistence.Expense
{
    public class ExpenseCalendarsConfiguration : IEntityTypeConfiguration<ExpenseCalendars>
    {
        public void Configure(EntityTypeBuilder<ExpenseCalendars> builder)
        {
            builder.ToTable("ExpenseCalendars");
            builder.HasKey(e => e.CalendarId);
        }
    }
}
