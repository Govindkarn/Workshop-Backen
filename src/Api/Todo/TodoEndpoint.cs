using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Todo;
public static class TodoEndpoint
{
    public static IEndpointRouteBuilder MapTodo(
        this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/hello", HelloWorldAsync);
        return routeBuilder;
    }

    public static async Task<Results<Ok<string>, BadRequest>> HelloWorldAsync()
    {
        await Task.CompletedTask;
        return TypedResults.Ok("Hello World!");
    }
}
