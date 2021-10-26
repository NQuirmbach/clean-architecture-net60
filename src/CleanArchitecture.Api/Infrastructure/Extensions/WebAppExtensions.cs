using CleanArchitecture.Api.Infrastructure.Routing;

namespace CleanArchitecture.Api.Infrastructure.Extensions;

public static class WebAppExtensions
{
    public static IApplicationBuilder MapRouteHandler<T>(this WebApplication app) where T : IEndpointMapper, new()
    {
        var mapper = Activator.CreateInstance<T>();

        mapper.Map(app);
        return app;
    }
}

