using Microsoft.Extensions.Caching.Memory;
using PipelineTask.PipelineTask.Api.Repositories;
using PipelineTask.PipelineTask.Api.Setups;
using PipelineTask.PipelineTask.Domain.Models;

namespace PipelineTask.PipelineTask.Infrastructure.Setups
{
    public class UserCacheSetup : IUserCacheSetup
    {
        private readonly IMemoryCache _memoryCache;
        private IEfRepository<User> _userRepository;

        public UserCacheSetup(IMemoryCache memoryCache, IEfRepository<User> userRepository)
        {
            _memoryCache = memoryCache;
            _userRepository = userRepository;
        }

        public async Task InitializeAsync()
        {
            var users = await _userRepository.GetAllAsync();

            foreach (var user in users)
                _memoryCache.Set(user.UserName, user);
        }
    }
}
