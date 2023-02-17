using MTask = TaskAPI.Core.Domain.Model.Task;

namespace TaskAPI.Core.Domain.RepositoryContracts;

public interface ITaskRepository
{
  Task<MTask> AddTaskAsync(MTask task);
  Task<MTask?> GetTaskByIdAsync(Guid id);
  Task<List<MTask>> GetAllAsync();
  Task UpdateTaskAsync(MTask task);
}
