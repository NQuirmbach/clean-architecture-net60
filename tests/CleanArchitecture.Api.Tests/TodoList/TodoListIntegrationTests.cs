using CleanArchitecture.Domain.Models;

using FluentAssertions;

using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using AutoFixture;

namespace CleanArchitecture.Api.Tests.TodoList
{
    public class TodoListIntegrationTests : ApiTest
    {

        public TodoListIntegrationTests(AppFixture fixture) : base(fixture)
        { }


        [Fact]
        public async Task Get_ReturnsListOfList()
        {
            // arrange
            var lists = Fixture.CreateMany<Domain.Entities.TodoList>();
            var context = GetDbContext();

            await context.AddRangeAsync(lists);
            await context.SaveChangesAsync();
            

            var client = CreateClient();

            // act
            var response = await client.GetFromJsonAsync<IEnumerable<TodoListDto>>("/todolist");

            // assert
            response.Should().NotBeNull();
            response.Count().Should().Equals(lists.Count());
        }
    }
}
