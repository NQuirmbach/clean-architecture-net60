using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using MockQueryable.Moq;

using Moq;

namespace CleanArchitecture.Application.Tests
{
    internal class Utils
    {
        public static Mock<DbSet<T>> CreateDbSetMock<T>(params T[] items) where T : class
        {
            return items.AsQueryable().BuildMockDbSet();
        }

        public static ILogger<T> CreateLogger<T>() where T: class
        {
            var factory = new NullLoggerFactory();

            return factory.CreateLogger<T>();
        }
    }
}
