using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApp.API.Data;
using TaskManagerWebApp.API.Data.Interfaces;
using Tasks = TaskManagerWebApp.API.Models.Tasks;

namespace TaskManagerWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {

        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("GetAllTasks")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return Ok(tasks);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] Tasks taskRequest)
        {
            string authorizationToken = HttpContext.Request.Headers["Authorization"];

            // Pass the authorization token to your repository method
            var tasks = await _taskRepository.AddTask(taskRequest, authorizationToken);

            //var tasks = await _taskRepository.AddTask(taskRequest);
            return Ok(tasks);
        }

        [HttpPut("EditTask")]
        public async Task<IActionResult> EditTask([FromBody] Tasks taskRequest)
        {
            var tasks = await _taskRepository.EditTask(taskRequest);
            return Ok(tasks);
        }

        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tasks = await _taskRepository.DeleteTask(id);
            return Ok(tasks);
        }

        [HttpGet("GetTaskDetailsById")]
        public async Task<IActionResult> GetTaskDetailsById(int id)
        {
            var tasks = await _taskRepository.GetTaskDetailsById(id);
            return Ok(tasks);
        }

    }
}
