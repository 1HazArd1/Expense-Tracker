using Microsoft.Extensions.DependencyInjection;

namespace Expense.Tracker.Infrastructure
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
