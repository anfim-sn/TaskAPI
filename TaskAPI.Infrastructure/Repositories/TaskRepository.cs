using Microsoft.EntityFrameworkCore;
using TaskAPI.Core.Domain.RepositoryContracts;
using MTask = TaskAPI.Core.Domain.Model.Task;

namespace TaskAPI.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
  private readonly ApplicationDbContext _context;

  public TaskRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<MTask?> GetTaskByIdAsync(Guid id)
  {
    var task = await _context.Task.FirstOrDefaultAsync(t => t.Id == id);
    return task;
  }

  public async Task<MTask> AddTaskAsync(MTask task)
  {
    Console.WriteLine("Add task: " + task);
    _context.Task.Add(task);
    await _context.SaveChangesAsync();
    return task;
  }

  public async Task UpdateTaskAsync(
    MTask task
  )
  {
    Console.WriteLine("Update task: " + task);
    _context.Update(task);
    await _context.SaveChangesAsync();
  }

  public Task<List<MTask>> GetAllAsync()
  {
    return _context.Task.ToListAsync();
  }
}
