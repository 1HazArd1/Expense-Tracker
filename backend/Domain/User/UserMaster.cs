using Expense.Tracker.Domain.Common;

namespace Expense.Tracker.Domain.User
{
    public class UserMaster : IEntity
    {
        public long UserId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte Status { get; set; }
    }
}