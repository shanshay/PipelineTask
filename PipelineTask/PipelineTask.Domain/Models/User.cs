using Microsoft.AspNetCore.Identity;
using PipelineTask.PipelineTask.Api.Models;

namespace PipelineTask.PipelineTask.Domain.Models
{
    public class User : IdentityUser, IBaseEntity
    {
        int IBaseEntity.Id { get; set; }

        public int UserId { get; set; }

        public string Login { get; set; }        

        public IList<ProcessTask> ProcessTasks { get; set; } = new List<ProcessTask>();
        
    }
}
