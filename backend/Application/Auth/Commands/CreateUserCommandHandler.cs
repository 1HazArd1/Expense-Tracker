using Expense.Tracker.Application.Common.Models;
using Expense.Tracker.Application.Common.Services.Cryptography;
using Expense.Tracker.Application.Exceptions;
using Expense.Tracker.Application.Interface.Persistence;
using Expense.Tracker.Application.Interface.Persistence.User;
using Expense.Tracker.Domain.User;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracker.Application.Auth.Commands
{
    public sealed record CreateUserCommand(User User) : IRequest;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(x => x.User.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.User.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.User.Password).NotNull().NotEmpty().MinimumLength(8);
        }
    }
    public class CreateUserCommandHandler(IUserMasterRepository userMasterRepository,
                                          IOLTPUnitOfWork unitOfWork,
                                          IPasswordHasher passwordHasher,
                                          IValidator<CreateUserCommand> validator) : IRequestHandler<CreateUserCommand>
    {
        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            string email = request.User.Email.Trim();
            UserMaster? existingUser = await userMasterRepository.GetAllAsNoTracking()
                                      .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            if (existingUser is not null)
                throw new BadRequestException("BE010102");
            

            UserMaster user = new()
            {
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                Email = email,
                Password = passwordHasher.HashPassword(request.User.Password),
                Status = 1,
                CreatedOn = DateTime.UtcNow,
            };

            await userMasterRepository.AddAsync(user, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
