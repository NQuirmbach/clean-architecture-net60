using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture;

using CleanArchitecture.Domain.Models;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Tests.Customize
{
    internal class TodoItemAddCustomizer : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<TodoItemAdd>(o => o
                .With(m => m.Status, fixture.Create<ItemStatus>().ToString())
            );
        }
    }
}
