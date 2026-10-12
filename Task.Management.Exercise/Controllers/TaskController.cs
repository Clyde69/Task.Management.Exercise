using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Exercise.Database.Repositories;
using TaskManagement.Exercise.Dtos;
using TaskManagement.Exercise.Enums;

namespace TaskManagement.Exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [HttpGet]
        [ProducesResponseType<IEnumerable<TaskDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _taskRepository.Get(Status.All));

        [HttpGet("{status}")]
        [ProducesResponseType<IEnumerable<TaskDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(Status status)
            => Ok(await _taskRepository.Get(status));

        [HttpPut]
        [ProducesResponseType<TaskDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(TaskDto task)
            => Ok(await _taskRepository.Create(task));

        [HttpPatch("{id}")]
        [ProducesResponseType<TaskDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Complete(int id)
        {
            var item = await _taskRepository.GetById(id);

            if (item is null)
            {
                throw new KeyNotFoundException($"A task with the id {id} is not present.");
            }
            item.Status = Status.Complete;
            return Ok(await _taskRepository.Update(item));
        }
    }
}
