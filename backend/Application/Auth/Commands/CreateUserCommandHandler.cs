using Expense.Tracker.Application.Common.Models;
using Expense.Tracker.Application.Common.Services.Cryptography;
using Expense.Tracker.Application.Exceptions;
using Expense.Tracker.Application.Interface.Persistence;
using Expense.Tracker.Application.Interface.Persistence.User;
using Expense.Tracker.Domain.User;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
        private static readonly Regex EmailRegex = new(
            @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        private static readonly Regex PasswordRegex = new(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?~`]).{8,128}$",
            RegexOptions.Compiled
        );

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            string email = request.User.Email.Trim();
            
            ValidateEmail(email);
            ValidatePassword(request.User.Password, email);

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

        private static void ValidateEmail(string email)
        {
            switch (email)
            {
                case null or "":
                    throw new BadRequestException("Email is required");
                case { Length: > 254 }:
                    throw new BadRequestException("Email address is too long");
            }

            if (!EmailRegex.IsMatch(email))
                throw new BadRequestException("Invalid email format");

            var emailLocalPart = email.Split('@')[0];
            if (emailLocalPart.Length > 64)
                throw new BadRequestException("Email local part is too long");
        }

        private static void ValidatePassword(string password, string email)
        {
            switch (password)
            {
                case null or "":
                    throw new BadRequestException("Password is required");
                case { Length: > 128 }:
                    throw new BadRequestException("Password is too long");
            }

            if (!PasswordRegex.IsMatch(password))
                throw new BadRequestException("Password must be 8-128 characters long and contain at least one lowercase letter, uppercase letter, digit, and special character");

            var emailLocalPart = email.Split('@')[0];
            if (password.Contains(emailLocalPart, StringComparison.OrdinalIgnoreCase))
                throw new BadRequestException("Password should not contain your email address");
        }
    }
}
