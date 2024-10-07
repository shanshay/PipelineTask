namespace PipelineTask.PipelineTask.Infrastructure.Exceptions
{
    public class AccessNotAllowedException : Exception
    {
        public AccessNotAllowedException(int id, string username)
            : base($"Access for update or delete task '{id}' is not allowed for user '{username}'.")
        {

        }
    }
}
