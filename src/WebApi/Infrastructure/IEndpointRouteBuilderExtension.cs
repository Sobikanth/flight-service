namespace WebApi.Infrastructure;

public static class IEndpointRouteBuilderExtension
{
    public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "")
    {
        builder.MapGet(pattern, handler)
            .WithName(handler.Method.Name);

        return builder;
    }
}