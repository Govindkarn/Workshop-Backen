namespace Api.Todo;

public sealed record TodoCreateRequest(
    string Name,
    bool IsCompleted);
