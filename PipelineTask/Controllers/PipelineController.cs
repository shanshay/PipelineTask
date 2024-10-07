using Microsoft.AspNetCore.Mvc;
using PipelineTask.PipelineTask.Api.Repositories;
using PipelineTask.PipelineTask.Domain.Models;
using PipelineTask.PipelineTask.Infrastructure.Exceptions;

namespace PipelineTask.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PipelineController : ControllerBase
    {
        private readonly IEfRepository<ProcessTask> _taskRepository;
        private readonly IEfRepository<Pipeline> _pipelineRepository;

        public PipelineController(
            IEfRepository<ProcessTask> taskRepository,
            IEfRepository<Pipeline> pipelineRepository)
        {
            _taskRepository = taskRepository;
            _pipelineRepository = pipelineRepository;
        }

        /// <summary>
        /// Get all pipelines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pipeline>>> GetALlAsync()
        {
            var pipeline = await _pipelineRepository.GetAllAsync();

            return Ok(pipeline);
        }

        /// <summary>
        /// Get pipeline by id
        /// int id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("pipeline/{id}")]
        public async Task<ActionResult<Pipeline>> GetByIdAsync(int id)
        {
            var task = _taskRepository.GetByIdAsync(id);

            return Ok(task);
        }

        [HttpPut]
        [Route("pipeline/{id}/edit")]
        public async Task<ActionResult<Pipeline>> UpdateAsync(int id, Pipeline updatedPipeline)
        {
            var pipeline = await _pipelineRepository.GetByIdAsync(id);
            if (pipeline is null)
                throw new EntityNotFoundException(id, nameof(Pipeline));

            pipeline.Name = updatedPipeline.Name;
            pipeline.DurationTimeHours = updatedPipeline.DurationTimeHours;
            pipeline.IsCompleted = updatedPipeline.IsCompleted;
            pipeline.ProcessTasks = updatedPipeline.ProcessTasks;

            return Ok(pipeline);
        }

        [HttpPost]
        [Route("pipeline/create")]
        public async Task<ActionResult<Pipeline>> CreateAsync(Pipeline newPipeline)
        {
            var pipeline = new Pipeline()
            {
                Name = newPipeline.Name,
                DurationTimeHours = newPipeline.DurationTimeHours,
                IsCompleted = newPipeline.IsCompleted,
                ProcessTasks = newPipeline.ProcessTasks
            };

            await _pipelineRepository.CreateAsync(pipeline);

            return Ok(pipeline);
        }

        [HttpDelete]
        [Route("pipeline/delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var pipeline = await _pipelineRepository.GetByIdAsync(id);
            if (pipeline is null)
                throw new EntityNotFoundException(id, nameof(Pipeline));

            await _pipelineRepository.DeleteAsync(pipeline);

            return Ok();
        }

        [HttpPost]
        [Route("pipeline/update/{id}")]
        public async Task<ActionResult<Pipeline>> CalculateAverageTime(int id)
        {
            var pipeline = await _pipelineRepository.GetByIdAsync(id);
            if (pipeline is null)
                throw new EntityNotFoundException(id, nameof(Pipeline));

            var tasks = pipeline.ProcessTasks.Where(t => t.IsCompleted);
            pipeline.DurationTimeHours = tasks.Sum(t => t.AverageTime);          
            
            return Ok(pipeline);
        }
    }
}
