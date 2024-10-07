using PipelineTask.PipelineTask.Api.Models;

namespace PipelineTask.PipelineTask.Domain.Models.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
