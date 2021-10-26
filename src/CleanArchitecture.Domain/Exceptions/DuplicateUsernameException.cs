namespace CleanArchitecture.Domain.Exceptions;

public class DuplicateUsernameException : Exception
{
    public DuplicateUsernameException(string username)
        : base($"Username '{username}' is already is use")
    { }
}
