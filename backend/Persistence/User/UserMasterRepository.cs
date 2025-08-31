using Expense.Tracker.Application.Interface.Persistence.User;
using Expense.Tracker.Domain.User;
using Expense.Tracker.Persistence.Shared;

namespace Expense.Tracker.Persistence.User
{
    public class UserMasterRepository(IOLTPDatabaseContext database) : Repository<UserMaster>(database), IUserMasterRepository
    {
    }
}