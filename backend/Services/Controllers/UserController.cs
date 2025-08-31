using Expense.Tracker.Application.Auth.Commands;
using Expense.Tracker.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Expense.Tracker.Services.Controllers
{
    [Route("/user")]
    public class UserController(ISender sender) : ApiControllerBase
    {
        [HttpPost]
        public async Task CreateUser([FromBody] User user)
        {
            await sender.Send(new CreateUserCommand(user));
        }
    }
}
    