using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;

using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Tests.Customize;
using CleanArchitecture.Application.TodoItems.Commands;
using CleanArchitecture.Domain.Entities;

using FluentAssertions;

using FluentValidation;

using Moq;

using Xunit;

namespace CleanArchitecture.Application.Tests.TodoItems.Commands;

public class CreateTodoItemCommandTests
{
    private readonly Mock<IValidator<TodoItem>> _validator;
    private readonly Fixture _fixture;
    private readonly Mock<IAppContext> _context;

    public CreateTodoItemCommandTests()
    {
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        fixture.Customize(new TodoItemAddCustomizer());

        _fixture = fixture;

        _context = new Mock<IAppContext>();
        _validator = new Mock<IValidator<TodoItem>>();
    }

    [Fact]
    public async Task Handle_ListDoesNotExist_ThrowsNotFoundException()
    {
        // arrange
        var listSet = Utils.CreateDbSetMock<TodoList>();

        _context.SetupGet(m => m.TodoLists).Returns(listSet.Object);

        var sut = new CreateTodoItemCommandHandler(
            _context.Object,
            _validator.Object,
            Utils.CreateLogger<CreateTodoItemCommand>()
        );

        var request = _fixture.Create<CreateTodoItemCommand>();

        // act
        await sut.Invoking(m => m.Handle(request, CancellationToken.None))
            .Should()
            .ThrowAsync<EntityNotFoundException>();

        // assert
    }

    [Fact]
    public async Task Handle_RequestIsComplete_ItemIsPersited()
    {
        // arrange
        var request = _fixture.Create<CreateTodoItemCommand>();
        var list = _fixture.Build<TodoList>()
            .With(m => m.Id, request.ListId)
            .Create();

        var listSet = Utils.CreateDbSetMock(list);

        _context.SetupGet(m => m.TodoLists).Returns(listSet.Object);

        var itemSet = Utils.CreateDbSetMock<TodoItem>();
        _context.Setup(m => m.TodoItems).Returns(itemSet.Object);

        var sut = new CreateTodoItemCommandHandler(
            _context.Object,
            _validator.Object,
            Utils.CreateLogger<CreateTodoItemCommand>()
        );

        // act
        var result = await sut.Handle(request, CancellationToken.None);

        // assert
        result.Should().NotBeNull();

        itemSet.Verify(m => m.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Once());
        _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
}

