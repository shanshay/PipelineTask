using PipelineTask.PipelineTask.Domain.Models.Base;

namespace PipelineTask.PipelineTask.Domain.Models
{
    public class ProcessTask : BaseEntity
    {
        public string Name { get; set; }

        public decimal AverageTime { get; set; }

        public bool IsCompleted { get; set; }

        public int UserId { get; set; }

        public IList<Pipeline> Pipelines { get; set; } = new List<Pipeline>();
    }
}
