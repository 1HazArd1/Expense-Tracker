namespace Expense.Tracker.Application.Common.Models
{
    public class User
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
