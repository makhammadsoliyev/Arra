using Arra.Api.Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Arra.Api.Extensions;

internal static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type =>
                type is { IsClass: true, IsAbstract: false, IsInterface: false } &&
                type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToList();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IEndpointRouteBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
            endpoint.MapEndpoint(builder);

        return app;
    }
}
