using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Core.ServiceContracts;

namespace TaskAPI.Api.Controllers;

[Route("/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
  private readonly ITaskService _service;

  public TaskController(
    ITaskService service
  )
  {
    _service = service;
  }

  // GET /task
  [HttpGet]
  public async Task<IActionResult> GetAllAsync()
  {
    var tasks = await _service.GetAllAsync();

    return Ok(tasks);
  }

  // GET /task/5
  [HttpGet("{id}")]
  public async Task<IActionResult> GetTaskAsync(Guid? id)
  {
    var task = await _service.GetTaskAsync(id);

    if (task == null)
      return NotFound("Task not found");

    return Ok(task);
  }

  // POST /task
  [HttpPost]
  public async Task<ObjectResult> CreateTaskAsync()
  {
    var task = await _service.AddTaskAsync();

    if (task == null)
      return StatusCode(500, new { message = "Some server error" });

    BackgroundWorker worker = new();
    worker.DoWork += (obj, e) => _service.CompleteTaskChainAsync(task);
    worker.RunWorkerAsync();

    var result = new { id = task.Id };
    return StatusCode(202, result);
  }
}
