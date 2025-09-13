using System.Security.Claims;

namespace Expense.Tracker.Application.Interface.Auth
{
    public interface ITokenService
    {
        string GenerateAcessToken(IEnumerable<Claim> claims);
    }
}
