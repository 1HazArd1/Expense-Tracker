using Expense.Tracker.Application.Common.Services.Auth;
using Expense.Tracker.Application.Common.Services.Cryptography;
using Expense.Tracker.Application.Interface.Persistence.User;
using Expense.Tracker.Domain.User;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracker.Application.Auth.Commands
{
    public record NativeLoginUserCommand(string Email, string Password) : IRequest<AuthSession>;

    public class NativeLoginUserCommandValidator : AbstractValidator<NativeLoginUserCommand>
    {
        public NativeLoginUserCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
    public class NativeLoginUserCommandHandler(IUserMasterRepository userMasterRepository,
                                               IValidator<NativeLoginUserCommand> validator,
                                               IPasswordHasher passwordHasher,
                                               IAuthService authService) : IRequestHandler<NativeLoginUserCommand, AuthSession>
    {
        public async Task<AuthSession> Handle(NativeLoginUserCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            UserMaster? user = await userMasterRepository.GetAllAsNoTracking()
                              .Where(x => x.Email == request.Email.Trim() &&
                                          x.Status == 1)
                              .SingleOrDefaultAsync(cancellationToken);

            if (user == null) 
                throw new UnauthorizedAccessException();

            if(!passwordHasher.VerifyPassword(request.Password, user.Password))
                 throw new UnauthorizedAccessException();

            return authService.PrepareAuthSession(user, cancellationToken);

        }
    }
}
