using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PipelineTask.PipelineTask.Api.Accessors;
using PipelineTask.PipelineTask.Api.Repositories;
using PipelineTask.PipelineTask.Domain.Models;
using PipelineTask.PipelineTask.Infrastructure.Exceptions;

namespace PipelineTask.Controllers
{
    [Controller]
    [Authorize]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IEfRepository<ProcessTask> _taskRepository;
        private readonly IEfRepository<Pipeline> _pipelineRepository;
        private readonly string _authenticatedUser;
        private readonly IUserCacheAccessor _userCacheAccessor;

        public TaskController(IEfRepository<ProcessTask> taskRepository, IHttpContextAccessor httpContextAccessor, IUserCacheAccessor userCacheAccessor)
        {
            _taskRepository = taskRepository;
            _authenticatedUser = httpContextAccessor.HttpContext.User.Identity.Name;
            _userCacheAccessor = userCacheAccessor;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessTask>>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();

            return Ok(tasks);
        }

        /// <summary>
        /// Get task by id
        /// int id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("task/{id}")]
        public async Task<ActionResult<ProcessTask>> GetByIdAsync(int id)
        {
            var task = _taskRepository.GetByIdAsync(id);

            return Ok(task);
        }

        /// <summary>
        /// Get all tasks by user id
        /// int user id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tasks/userId/{userId}")]
        public async Task<ActionResult<IEnumerable<ProcessTask>>> GetByUserIdAsync(int userId)
        {
            var tasks = await _taskRepository.GetAllAsync();
            var response = tasks.Where(t => t.UserId == userId);

            return Ok(response);
        }

        [HttpGet]
        [Route("tasks/pipelineId/{pipelineId}")]
        /// <summary>
        /// Get all tasks by pipeline id
        /// int pipeline id
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<ProcessTask>>> GetByPipelineIdAsync(int pipelineId)
        {
            var pipelines = await _pipelineRepository.GetByIdAsync(pipelineId);

            return Ok(pipelines.ProcessTasks);
        }

        [HttpPut]
        [Route("task/{id}/edit")]
        public async Task<ActionResult<ProcessTask>> UpdateAsync(int id, ProcessTask updatedTask)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task is null)
                throw new EntityNotFoundException(id, nameof(ProcessTask));

            var user = _userCacheAccessor.GetUser(_authenticatedUser);
            if (user.UserId != task.UserId)
                throw new AccessNotAllowedException(id, _authenticatedUser);

            task.Name = updatedTask.Name;
            task.AverageTime = updatedTask.AverageTime;
            task.IsCompleted = updatedTask.IsCompleted;

            return Ok(task);
        }

        [HttpPost]
        [Route("task/create")]
        public async Task<ActionResult<ProcessTask>> CreateAsync(ProcessTask newTask)
        {
            var task = new ProcessTask()
            {
                Name = newTask.Name,
                AverageTime = newTask.AverageTime,
                IsCompleted = newTask.IsCompleted,
                UserId = newTask.UserId
            };

            await _taskRepository.CreateAsync(task);

            return Ok(task);
        }

        [HttpDelete]
        [Route("task/delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task is null)
                throw new EntityNotFoundException(id, nameof(ProcessTask));

            var user = _userCacheAccessor.GetUser(_authenticatedUser);
            if (user.UserId != task.UserId)
                throw new AccessNotAllowedException(id, _authenticatedUser);

            await _taskRepository.DeleteAsync(task);

            return Ok();
        }

        [HttpPut]
        [Route("task/update/{id}")]
        public async Task<ActionResult<ProcessTask>> AddTaskToPipelineAsync(int id, Pipeline pipeline)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task is null)
                throw new EntityNotFoundException(id, nameof(ProcessTask));

            pipeline.ProcessTasks.Add(task);

            await _pipelineRepository.UpdateAsync(pipeline);

            return Ok(task);
        }

        [HttpPut]
        [Route("task/update/{id}")]
        public async Task<ActionResult<ProcessTask>> DeleteTaskFromPipelineAsync(int id, Pipeline pipeline)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task is null)
                throw new EntityNotFoundException(id, nameof(ProcessTask));

            pipeline.ProcessTasks.Remove(task);

            await _pipelineRepository.UpdateAsync(pipeline);

            return Ok(task);
        }
    }
}
