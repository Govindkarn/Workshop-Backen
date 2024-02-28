using Data.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Todo;
public static class TodoEndpoint
{
    public static IEndpointRouteBuilder MapTodo(
        this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/hello", HelloWorldAsync);
        routeBuilder.MapGet("/api/todo/{id}", GetTodoByIdAsync);
        routeBuilder.MapGet("/api/todo/get", GetAllTodoAsync);
        routeBuilder.MapPost("/api/todo/create", CreateTodoAsync);
        routeBuilder.MapDelete("/api/todo/delete/{id}", DeleteTodoAsync);
        return routeBuilder;
    }

    public static async Task<Results<Ok<string>, BadRequest>> HelloWorldAsync()
    {
        await Task.CompletedTask;
        return TypedResults.Ok("Hello World!");
    }

    public static async Task<Results<Ok,BadRequest>> CreateTodoAsync(
        TodoCreateRequest request,
        ITodoRepository todoRepository,
        CancellationToken cancellationToken)
    {
        todoRepository.Add(new Data.Todos.Todo{
            Id = Guid.NewGuid(),
            Name = request.Name,
            IsCompletd = request.IsCompleted
        });

        await Task.CompletedTask;

        return TypedResults.Ok();
    }

    public static async Task<Results<Ok<Data.Todos.Todo>, BadRequest>> GetTodoByIdAsync(
        Guid id, 
        ITodoRepository todoRepository,
        CancellationToken cancellationToken)
    {
        var todo = await todoRepository.GetTodoByIdAsync(id, cancellationToken);

        if(todo is not null)
        {
            return TypedResults.Ok(todo);
        }

        return TypedResults.BadRequest();
    }

    public static async Task<Results<Ok<IReadOnlyList<Data.Todos.Todo?>>, BadRequest>> GetAllTodoAsync(
        ITodoRepository todoRepository,
        CancellationToken cancellationToken)
    {
        var todos = await todoRepository.GetAllTodoAsync(cancellationToken);
        if(todos is not null)
        {
            return TypedResults.Ok(todos);
        }

        return TypedResults.BadRequest();
    }

    public static async Task<Results<Ok,BadRequest>> DeleteTodoAsync(
        Guid id,
        ITodoRepository todoRepository,
        CancellationToken cancellationToken)
    {
        todoRepository.Delete(id);
        await Task.CompletedTask;
        return TypedResults.Ok();
    }
}
