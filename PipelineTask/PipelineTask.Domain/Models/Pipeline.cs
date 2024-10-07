using PipelineTask.PipelineTask.Domain.Models.Base;

namespace PipelineTask.PipelineTask.Domain.Models
{
    public class Pipeline : BaseEntity
    {
        public string Name { get; set; }

        public decimal DurationTimeHours { get; set; }

        public bool IsCompleted { get; set; }

        public IList<ProcessTask> ProcessTasks { get; set; } = new List<ProcessTask>();
    }
}
