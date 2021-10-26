namespace CleanArchitecture.Application.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, object entityKey)
        : base($"Entity '{entityName}' with key '{entityKey}' was not found")
    {
        EntityName = entityName;
        EntityKey = entityKey;
    }

    public string EntityName { get; }
    public object EntityKey { get; }
}
