using TaskAPI.Core.Domain.Enum;
using TaskAPI.Core.Domain.RepositoryContracts;
using TaskAPI.Core.DTO;
using TaskAPI.Core.ServiceContracts;
using MTask = TaskAPI.Core.Domain.Model.Task;

namespace TaskAPI.Core;

public class TaskService : ITaskService
{
  private readonly ITaskRepository _repository;

  public TaskService(ITaskRepository repository)
  {
    _repository = repository;
  }

  public async Task<MTask> AddTaskAsync()
  {
    MTask task = new();

    await _repository.AddTaskAsync(task);

    return task;
  }

  public async Task ChangeTaskStatusAsync(MTask task, StatusEnum status)
  {
    task.Status = status;
    task.TimeStamp = DateTime.Now.Ticks;
    await _repository.UpdateTaskAsync(task);
  }

  public async Task<TaskResponse?> GetTaskAsync(Guid? id)
  {
    if (id == null) return null;

    var task = await _repository.GetTaskByIdAsync(id.Value);

    return task?.ToTaskResponse();
  }

  public async Task<List<MTask>> GetAllAsync()
  {
    var tasks = await _repository.GetAllAsync();

    return tasks;
  }

  public async Task CompleteTaskChainAsync(object taskObj)
  {
    try
    {
      var task = (MTask)taskObj;

      await ChangeTaskStatusAsync(task, StatusEnum.Running);
      Thread.Sleep(120000);
      Console.WriteLine(task);
      await ChangeTaskStatusAsync(task, StatusEnum.Finished);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }
}
