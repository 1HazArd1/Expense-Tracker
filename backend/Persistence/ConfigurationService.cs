using Expense.Tracker.Application.Interface.Persistence;
using Expense.Tracker.Application.Interface.Persistence.Expense;
using Expense.Tracker.Persistence.Expense;
using Expense.Tracker.Persistence.Shared;
using Expense.Tracker.Persistence.Shared.OLTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Expense.Tracker.Persistence
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<OLTPDatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("hazArdConnectionString")), ServiceLifetime.Scoped);

            //OLTP
            services.AddScoped(typeof(IOLTPDatabaseContext), typeof(OLTPDatabaseContext));
            services.AddScoped(typeof(IOLTPUnitOfWork), typeof(OLTPUnitOfWork));

            //Expense
            services.AddScoped(typeof(IExpenseCalendarsRepository), typeof(ExpenseCalendarsRepository));

            return services;
        }
    }
}
