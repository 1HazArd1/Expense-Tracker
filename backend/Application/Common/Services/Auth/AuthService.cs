using Expense.Tracker.Application.Interface.Auth;
using Expense.Tracker.Domain.User;
using System.Security.Claims;

namespace Expense.Tracker.Application.Common.Services.Auth
{
    public interface IAuthService
    {
        AuthSession PrepareAuthSession(UserMaster user, CancellationToken cancellationToken);
    }
    public record AuthSession(long UserId, string Email, string FirstName, string? LastName, string AccessToken);

    public class AuthService(ITokenService tokenService) : IAuthService
    {
        public AuthSession PrepareAuthSession(UserMaster user, CancellationToken cancellationToken)
        {
            string accessToken = GenerateToken(user);
            return new AuthSession(user.UserId, user.Email, user.FirstName, user.LastName, accessToken);

        }
        public string GenerateToken(UserMaster user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.UserId.ToString())
            ];

            return tokenService.GenerateAcessToken(claims);
        }
    }
}
