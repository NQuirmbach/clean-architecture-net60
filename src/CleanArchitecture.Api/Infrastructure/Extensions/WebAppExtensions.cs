using CleanArchitecture.Api.Infrastructure.Routing;
using CleanArchitecture.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Api.Infrastructure.Extensions;

public static class WebAppExtensions
{
    public static WebApplication MapRouteHandler<T>(this WebApplication app) where T : IEndpointMapper, new()
    {
        var mapper = Activator.CreateInstance<T>();

        mapper.Map(app);
        return app;
    }

    public static WebApplicationBuilder MigrateDatabase(this WebApplicationBuilder builder)
    {
        using var scope = builder.Services.BuildServiceProvider().CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            logger.LogInformation("Database migrated successfully");

        } catch (Exception ex)
        {
            logger.LogError(ex, "Error while migrating database");
        }

        return builder;
    }
}

