using System;
using System.Linq.Expressions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Domain.Models
{
    public static partial class TodoListMapper
    {
        public static TodoListDto AdaptToDto(this TodoList p1)
        {
            return p1 == null ? null : new TodoListDto()
            {
                Name = p1.Name,
                Items = null,
                Id = p1.Id,
                CreationDate = p1.CreationDate
            };
        }
        public static TodoListDto AdaptTo(this TodoList p2, TodoListDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            TodoListDto result = p3 ?? new TodoListDto();
            
            result.Name = p2.Name;
            result.Items = null;
            result.Id = p2.Id;
            result.CreationDate = p2.CreationDate;
            return result;
            
        }
        public static Expression<Func<TodoList, TodoListDto>> ProjectToDto => p4 => new TodoListDto()
        {
            Name = p4.Name,
            Items = null,
            Id = p4.Id,
            CreationDate = p4.CreationDate
        };
        public static TodoList AdaptToTodoList(this TodoListAdd p5)
        {
            return p5 == null ? null : new TodoList() {Name = p5.Name};
        }
        public static TodoList AdaptTo(this TodoListUpdate p6, TodoList p7)
        {
            if (p6 == null)
            {
                return null;
            }
            TodoList result = p7 ?? new TodoList();
            
            result.Name = p6.Name;
            return result;
            
        }
        public static TodoList AdaptTo(this TodoListMerge p8, TodoList p9)
        {
            if (p8 == null)
            {
                return null;
            }
            TodoList result = p9 ?? new TodoList();
            
            if (p8.Name != null)
            {
                result.Name = p8.Name;
            }
            return result;
            
        }
    }
}