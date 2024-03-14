namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {

        // services.AddHttpContextAccessor();

        // services.AddEndpointsApiExplorer();

        return services;
    }
}