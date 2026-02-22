using Microsoft.AspNetCore.Mvc;

namespace netcore.Modules.Tasks
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _service;

        public TaskController(TaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _service.GetAllAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateDto dto)
        {
            var task = await _service.CreateAsync(dto);
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, TaskCreateDto dto)
        {
            // Use the ID from the URL to ensure we update the right record
            var updatedTask = await _service.UpdateAsync(id, dto);
            
            if (updatedTask == null) return NotFound();
            
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            
            if (!result) return NotFound();

            return NoContent(); // Returns 204 (Successful delete, no content to return)
        }
    }
}