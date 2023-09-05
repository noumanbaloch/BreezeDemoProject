using Breeze.API.AutoMappingProfile;
using Breeze.DbCore.Context;
using Breeze.DbCore.UnitOfWork;
using Breeze.Services.Account;
using Breeze.Services.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjections
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMappingProfile).Assembly);

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!);
            });

            return services;
        }
    }
}
