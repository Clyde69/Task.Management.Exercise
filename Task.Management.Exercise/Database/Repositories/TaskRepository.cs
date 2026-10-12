using TaskManagement.Exercise.Database.Contexts;
using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Enums;

namespace TaskManagement.Exercise.Database.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _dbContext;

        public TaskRepository(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IQueryable<TaskDto>> Get(Status filter = Status.All)
        {
            return Task.Run(() =>
            {
                return _dbContext.Tasks.Where(w =>
                    filter.Equals(Status.All)
                    ||
                    w.Status.Equals(filter));
            });
        }

        public Task<TaskDto?> GetById(int id)
        {
            return Task.Run(() =>
            {
                return _dbContext.Tasks.FirstOrDefault(w => w.Id.Equals(id));
            });
        }

        public async Task<TaskDto> Create(TaskDto newTask)
        {
            newTask.Id = null;
            _dbContext.Tasks.Add(newTask);
            await _dbContext.SaveChangesAsync();
            return newTask;
        }

        public async Task<TaskDto> Update(TaskDto task)
        {
            if (!_dbContext.Tasks.Any(w => w.Id.Equals(task.Id)))
            {
                throw new KeyNotFoundException("No item in the database could be found to update.");
            }

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }
    }
}
