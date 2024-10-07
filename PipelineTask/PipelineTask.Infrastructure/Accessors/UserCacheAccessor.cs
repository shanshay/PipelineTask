using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using PipelineTask.PipelineTask.Api.Accessors;
using PipelineTask.PipelineTask.Domain.Models;

namespace PipelineTask.PipelineTask.Infrastructure.Accessors
{
    public class UserCacheAccessor : IUserCacheAccessor
    {
        private readonly IMemoryCache _cache;

        public UserCacheAccessor(IMemoryCache cache)
        {
            _cache = cache; 
        }

        public User GetUser(string username)
        {
            return _cache.TryGetValue(username, out User user)
                ? user
                : null;
        }
    }
}
