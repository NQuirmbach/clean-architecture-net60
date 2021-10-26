
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Infrastructure;

public class MachineDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}
