using Microsoft.EntityFrameworkCore;
using PipelineTask.PipelineTask.Domain.Models;

namespace PipelineTask.PipelineTask.Domain.DataAccess
{
    public partial class PipelineTaskContext : DbContext
    {
        public PipelineTaskContext(DbContextOptions<PipelineTaskContext> options)
            : base(options) 
        {
        }

        public virtual DbSet<ProcessTask> ProcessTasks { get; set; }

        public virtual DbSet<Pipeline> Pipelines { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
