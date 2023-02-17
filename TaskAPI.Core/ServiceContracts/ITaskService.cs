using TaskAPI.Core.Domain.Enum;
using TaskAPI.Core.DTO;
using MTask = TaskAPI.Core.Domain.Model.Task;

namespace TaskAPI.Core.ServiceContracts;

public interface ITaskService
{
  public Task<MTask> AddTaskAsync();
  public Task<TaskResponse?> GetTaskAsync(Guid? id);
  public Task<List<MTask>> GetAllAsync();

  public Task ChangeTaskStatusAsync(MTask task, StatusEnum status);

  public Task CompleteTaskChainAsync(object taskObj);
}
