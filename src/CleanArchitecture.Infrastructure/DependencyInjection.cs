using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Perstience;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<AppDbContext>(o =>
        {
            o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        services.AddScoped<IAppContext, AppDbContext>();

        services.AddTransient<IDateTimeProvider, MachineDateTimeProvider>();
    }
}
