using PipelineTask.PipelineTask.Domain.Models;

namespace PipelineTask.PipelineTask.Api.Accessors
{
    public interface IUserCacheAccessor
    {
        public User GetUser(string username);
    }
}
