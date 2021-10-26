
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;

public class TodoItem : AppEntity
{
    public string Text { get; set; }
    public string? Description { get; set; }
    public ItemStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
    
    public Guid ListId { get; set; }
    public TodoList List { get; set; }
}

