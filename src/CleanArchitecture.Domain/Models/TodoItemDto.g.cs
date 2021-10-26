using System;

namespace CleanArchitecture.Domain.Models
{
    public partial class TodoItemDto
    {
        public string Text { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}