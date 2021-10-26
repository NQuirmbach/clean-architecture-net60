using System;
using System.Collections.Generic;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Domain.Models
{
    public partial class TodoListDto
    {
        public string Name { get; set; }
        public ICollection<TodoItemDto> Items { get; set; }
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}