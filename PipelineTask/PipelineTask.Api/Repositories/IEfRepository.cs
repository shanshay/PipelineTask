using PipelineTask.PipelineTask.Api.Models;
using PipelineTask.PipelineTask.Domain.Models.Base;

namespace PipelineTask.PipelineTask.Api.Repositories
{
    public interface IEfRepository<T> where T : IBaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(int id);

        public Task<IEnumerable<T>> GetRangeByIdsAsync(IEnumerable<int> ids);

        public Task<IEnumerable<T>> GetRangeByIdAsync(int id);

        public Task CreateAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}
