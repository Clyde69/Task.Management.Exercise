using TaskManagement.Exercise.Database.Contexts;
using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Enums;

namespace TaskManagement.Exercise.Database.Repositories
{
    public class TaskRepository
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
    }
}
