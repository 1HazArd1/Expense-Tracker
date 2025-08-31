using Expense.Tracker.Application.Common.Services.Cryptography;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Expense.Tracker.Application
{
    public static class ConfigurationService
    {
        public static IServiceCollection  AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IPasswordHasher), typeof(PasswordHasher));
            return services;
        }
    }
}
