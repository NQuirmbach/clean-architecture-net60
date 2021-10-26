using System;
using System.Linq.Expressions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Models;
using Mapster.Utils;

namespace CleanArchitecture.Domain.Models
{
    public static partial class TodoItemMapper
    {
        public static TodoItemDto AdaptToDto(this TodoItem p1)
        {
            return p1 == null ? null : new TodoItemDto()
            {
                Text = p1.Text,
                Description = p1.Description,
                Status = Enum<ItemStatus>.ToString(p1.Status),
                DueDate = p1.DueDate,
                Id = p1.Id,
                CreationDate = p1.CreationDate
            };
        }
        public static TodoItemDto AdaptTo(this TodoItem p2, TodoItemDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            TodoItemDto result = p3 ?? new TodoItemDto();
            
            result.Text = p2.Text;
            result.Description = p2.Description;
            result.Status = Enum<ItemStatus>.ToString(p2.Status);
            result.DueDate = p2.DueDate;
            result.Id = p2.Id;
            result.CreationDate = p2.CreationDate;
            return result;
            
        }
        public static Expression<Func<TodoItem, TodoItemDto>> ProjectToDto => p4 => new TodoItemDto()
        {
            Text = p4.Text,
            Description = p4.Description,
            Status = Enum<ItemStatus>.ToString(p4.Status),
            DueDate = p4.DueDate,
            Id = p4.Id,
            CreationDate = p4.CreationDate
        };
        public static TodoItem AdaptToTodoItem(this TodoItemAdd p5)
        {
            return p5 == null ? null : new TodoItem()
            {
                Text = p5.Text,
                Description = p5.Description,
                Status = p5.Status == null ? ItemStatus.Todo : Enum<ItemStatus>.Parse(p5.Status),
                DueDate = p5.DueDate
            };
        }
        public static TodoItem AdaptTo(this TodoItemUpdate p6, TodoItem p7)
        {
            if (p6 == null)
            {
                return null;
            }
            TodoItem result = p7 ?? new TodoItem();
            
            result.Text = p6.Text;
            result.Description = p6.Description;
            result.Status = p6.Status == null ? ItemStatus.Todo : Enum<ItemStatus>.Parse(p6.Status);
            result.DueDate = p6.DueDate;
            return result;
            
        }
        public static TodoItem AdaptTo(this TodoItemMerge p8, TodoItem p9)
        {
            if (p8 == null)
            {
                return null;
            }
            TodoItem result = p9 ?? new TodoItem();
            
            if (p8.Text != null)
            {
                result.Text = p8.Text;
            }
            
            if (p8.Description != null)
            {
                result.Description = p8.Description;
            }
            
            if (p8.Status != null)
            {
                result.Status = Enum<ItemStatus>.Parse(p8.Status);
            }
            
            if (p8.DueDate != null)
            {
                result.DueDate = p8.DueDate;
            }
            return result;
            
        }
    }
}