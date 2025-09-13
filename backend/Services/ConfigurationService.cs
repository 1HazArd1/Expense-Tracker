using Expense.Tracker.Application.Interface.Auth;
using Expense.Tracker.Services.Filters;
using Expense.Tracker.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Expense.Tracker.Services
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            // Add other services here as needed
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add exception handling
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.SetAuthentication(builder.Configuration);

            services.AddScoped(typeof(ITokenService), typeof(JWTTokenService));

            return services;
        }

        public static void SetAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                };
            });

            services.AddAuthorization();
        }
    }
}
