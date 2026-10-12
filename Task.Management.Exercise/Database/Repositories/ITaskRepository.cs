using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Enums;

namespace TaskManagement.Exercise.Database.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskDto> Create(TaskDto newTask);
        Task<IQueryable<TaskDto>> Get(Status filter = Status.All);
        Task<TaskDto?> GetById(int id);
        Task<TaskDto> Update(TaskDto task);
    }
}