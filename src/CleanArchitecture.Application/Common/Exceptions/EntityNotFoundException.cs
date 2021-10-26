namespace CleanArchitecture.Application.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, object entityKey)
        : base($"Entity '{entityName}' with key '{entityKey}' was not found")
    {

    }
}
