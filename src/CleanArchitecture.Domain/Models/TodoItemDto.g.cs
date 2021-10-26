using System;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Domain.Models
{
    public partial class TodoItemDto
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ListId { get; set; }
        public TodoListDto List { get; set; }
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}