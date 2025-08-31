using Expense.Tracker.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expense.Tracker.Persistence.User
{
    public class UserMasterConfiguration : IEntityTypeConfiguration<UserMaster>
    {
        public void Configure(EntityTypeBuilder<UserMaster> builder)
        {
            builder.ToTable("UserMaster");
            builder.HasKey(u => u.UserId);
            builder.HasQueryFilter(u => u.Status == 1);

        }
    }
}