using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities;

public class TodoList : AppEntity
{
    public string Name { get; set; }
    public ICollection<TodoItem> Items { get; set; } = new List<TodoItem>();


    public TodoList() { }

    public TodoList(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        Name = name;
    }
}
