namespace PipelineTask.PipelineTask.Infrastructure.Exceptions
{
    public class EntityNotFoundException : Exception        
    {
        public EntityNotFoundException(int entityId, string entityType)
            : base($"Entity '{entityType}' with '{entityId}' doesn't exist.")
        {

        }
    }
}
