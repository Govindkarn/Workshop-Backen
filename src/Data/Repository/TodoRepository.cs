using Data.Todos;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;
internal sealed class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TodoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Add(Todo todo)
    {
        _dbContext.Todos.Add(todo);
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        Todo? todo = GetTodoByIdAsync(id)
            .GetAwaiter()
            .GetResult();

        if(todo is null)
        {
            return;
        }

        _dbContext.Todos.Remove(todo);
        _dbContext.SaveChanges();
    }

    public async Task<IReadOnlyList<Todo?>> GetAllTodoAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Todo?> todos = await _dbContext
            .Todos
            .ToListAsync(cancellationToken);

        return todos;
    }

    public async Task<Todo?> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Todo? todo = await _dbContext
            .Todos
            .FirstOrDefaultAsync(
                todo => todo.Id == id, 
                cancellationToken);

        return todo;
    }
}
