using System;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Models
{
    public partial class TodoItemMerge
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public ItemStatus? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? ListId { get; set; }
    }
}