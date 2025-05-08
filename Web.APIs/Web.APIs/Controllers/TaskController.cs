using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Application.DTOs.AccountDTO;
using Web.Application.DTOs.TaskDTO;
using Web.Application.Interfaces;
using Web.Application.Response;
using Web.Infrastructure.Service;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> AddTaskAsync(AddTaskDto task)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _taskService.CreateTaskAsync(userId, task);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            var result = await _taskService.GetAllTasks();
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById([FromRoute] int id)
        {
            var result = await _taskService.GetTaskByIdAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskById([FromRoute]int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] int id,AddTaskDto addTaskDto)
        {
            var result = await _taskService.UpdateTaskAsync(id,addTaskDto);
            return result.Success ? Ok(result) : BadRequest(result);

        }

    }
}
