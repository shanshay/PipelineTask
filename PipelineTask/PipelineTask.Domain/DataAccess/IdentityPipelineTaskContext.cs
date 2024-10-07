using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PipelineTask.PipelineTask.Domain.Models;

namespace PipelineTask.PipelineTask.Domain.DataAccess
{
    public class IdentityPipelineTaskContext : IdentityDbContext<User>
    {
        public IdentityPipelineTaskContext(DbContextOptions<IdentityPipelineTaskContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
