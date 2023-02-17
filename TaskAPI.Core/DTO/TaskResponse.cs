using TaskAPI.Core.Domain.Enum;
using Task = TaskAPI.Core.Domain.Model.Task;

namespace TaskAPI.Core.DTO;

public class TaskResponse
{
  public string Status { get; set; }
  public string TimeStamp { get; set; }
}

public static class TaskExtensions
{
  public static TaskResponse ToTaskResponse(this Task task)
  {
    return new TaskResponse
    {
      Status = Enum.GetName(typeof(StatusEnum), task.Status),
      TimeStamp =
        new DateTime(task.TimeStamp).ToString("yyyy-MM-ddTHH:mm:ss")
    };
  }
}
