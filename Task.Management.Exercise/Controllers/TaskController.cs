using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Exercise.Dtos;

namespace TaskManagement.Exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult Complete()
        {
            throw new NotImplementedException();
        }
    }
}
