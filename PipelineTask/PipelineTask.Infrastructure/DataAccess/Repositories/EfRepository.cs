using Microsoft.EntityFrameworkCore;
using PipelineTask.PipelineTask.Api.Repositories;
using PipelineTask.PipelineTask.Domain.DataAccess;
using PipelineTask.PipelineTask.Domain.Models.Base;

namespace PipelineTask.PipelineTask.Infrastructure.DataAccess.Repositories
{
    public class EfRepository<T> : IEfRepository<T> where T : BaseEntity
    {
        private readonly PipelineTaskContext _dataContext;

        public EfRepository(PipelineTaskContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dataContext.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(IEnumerable<int> ids)
        {
            var entities = await _dataContext.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetRangeByIdAsync(int id)
        {
            var entities = await _dataContext.Set<T>().Where(x => x.Id== id).ToListAsync();

            return entities;
        }

        public async Task UpdateAsync(T entity)
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
