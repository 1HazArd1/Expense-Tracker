using Expense.Tracker.Application.Auth.Commands;
using Expense.Tracker.Application.Common.Models.Auth;
using Expense.Tracker.Application.Common.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expense.Tracker.Services.Controllers
{
    [Route("user")]
    public class UserController(ISender sender) : ApiControllerBase
    {
        [HttpPost]
        public async Task CreateUser([FromBody] User user)
        {
            await sender.Send(new CreateUserCommand(user));
        }

        [HttpPost("login")]
        public async Task<AuthSession> NativeLogin(NativeLoginUserCommand loginUserCommand)
        {
            return await sender.Send(loginUserCommand);
        }
    }
}
    