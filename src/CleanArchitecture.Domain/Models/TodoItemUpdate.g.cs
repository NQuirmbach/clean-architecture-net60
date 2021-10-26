using System;

namespace CleanArchitecture.Domain.Models
{
    public partial class TodoItemUpdate
    {
        public string Text { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}