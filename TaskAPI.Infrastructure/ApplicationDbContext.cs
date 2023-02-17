using Microsoft.EntityFrameworkCore;
using MTask = TaskAPI.Core.Domain.Model.Task;

namespace TaskAPI.Infrastructure;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions options) : base(options) { }

  public DbSet<MTask> Task { get; set; }
}
