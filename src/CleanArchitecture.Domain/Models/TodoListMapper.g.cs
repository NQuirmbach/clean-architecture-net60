using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Models;
using Mapster.Utils;

namespace CleanArchitecture.Domain.Models
{
    public static partial class TodoListMapper
    {
        public static TodoListDto AdaptToDto(this TodoList p1)
        {
            return p1 == null ? null : new TodoListDto()
            {
                Name = p1.Name,
                Items = funcMain1(p1.Items),
                Id = p1.Id,
                CreationDate = p1.CreationDate
            };
        }
        public static TodoListDto AdaptTo(this TodoList p3, TodoListDto p4)
        {
            if (p3 == null)
            {
                return null;
            }
            TodoListDto result = p4 ?? new TodoListDto();
            
            result.Name = p3.Name;
            result.Items = funcMain2(p3.Items, result.Items);
            result.Id = p3.Id;
            result.CreationDate = p3.CreationDate;
            return result;
            
        }
        public static Expression<Func<TodoList, TodoListDto>> ProjectToDto => p7 => new TodoListDto()
        {
            Name = p7.Name,
            Items = p7.Items.Select<TodoItem, TodoItemDto>(p8 => new TodoItemDto()
            {
                Text = p8.Text,
                Description = p8.Description,
                Status = Enum<ItemStatus>.ToString(p8.Status),
                DueDate = p8.DueDate,
                Id = p8.Id,
                CreationDate = p8.CreationDate
            }).ToList<TodoItemDto>(),
            Id = p7.Id,
            CreationDate = p7.CreationDate
        };
        public static TodoList AdaptToTodoList(this TodoListAdd p9)
        {
            return p9 == null ? null : new TodoList() {Name = p9.Name};
        }
        public static TodoList AdaptTo(this TodoListUpdate p10, TodoList p11)
        {
            if (p10 == null)
            {
                return null;
            }
            TodoList result = p11 ?? new TodoList();
            
            result.Name = p10.Name;
            return result;
            
        }
        public static TodoList AdaptTo(this TodoListMerge p12, TodoList p13)
        {
            if (p12 == null)
            {
                return null;
            }
            TodoList result = p13 ?? new TodoList();
            
            if (p12.Name != null)
            {
                result.Name = p12.Name;
            }
            return result;
            
        }
        
        private static ICollection<TodoItemDto> funcMain1(ICollection<TodoItem> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            ICollection<TodoItemDto> result = new List<TodoItemDto>(p2.Count);
            
            IEnumerator<TodoItem> enumerator = p2.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                TodoItem item = enumerator.Current;
                result.Add(item == null ? null : new TodoItemDto()
                {
                    Text = item.Text,
                    Description = item.Description,
                    Status = Enum<ItemStatus>.ToString(item.Status),
                    DueDate = item.DueDate,
                    Id = item.Id,
                    CreationDate = item.CreationDate
                });
            }
            return result;
            
        }
        
        private static ICollection<TodoItemDto> funcMain2(ICollection<TodoItem> p5, ICollection<TodoItemDto> p6)
        {
            if (p5 == null)
            {
                return null;
            }
            ICollection<TodoItemDto> result = new List<TodoItemDto>(p5.Count);
            
            IEnumerator<TodoItem> enumerator = p5.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                TodoItem item = enumerator.Current;
                result.Add(item == null ? null : new TodoItemDto()
                {
                    Text = item.Text,
                    Description = item.Description,
                    Status = Enum<ItemStatus>.ToString(item.Status),
                    DueDate = item.DueDate,
                    Id = item.Id,
                    CreationDate = item.CreationDate
                });
            }
            return result;
            
        }
    }
}