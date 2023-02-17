using TaskAPI.Core.Domain.Enum;

namespace TaskAPI.Core.Domain.Model;

public class Task
{
  public Task()
  {
    Id = Guid.NewGuid();
    TimeStamp = DateTime.Now.Ticks;
    Status = StatusEnum.Created;
  }

  public Guid Id { get; set; }
  public long TimeStamp { get; set; }
  public StatusEnum Status { get; set; }

  public override string ToString()
  {
    return $"Id: {Id}, TimeStamp: {TimeStamp}, Status: {Status.ToString()}";
  }
}
