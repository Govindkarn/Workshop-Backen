using Data.Todos;

namespace Data.Repository;
public interface ITodoRepository
{
    public Task<Todo?> GetTodoByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default);
    
    public Task<IReadOnlyList<Todo?>> GetAllTodoAsync(
        CancellationToken cancellationToken = default);
    public void Add(Todo todo);
    public void Delete(Guid id);
}
