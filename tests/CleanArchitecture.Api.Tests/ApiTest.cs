using System.Linq;
using System.Net.Http;

using AutoFixture;

using CleanArchitecture.Infrastructure.Persistence;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace CleanArchitecture.Api.Tests
{
    public abstract class ApiTest : IClassFixture<AppFixture>
    {
        protected IFixture Fixture { get; }
        private readonly AppFixture _appFixture;


        public ApiTest(AppFixture appFixture)
        {
            _appFixture = appFixture ?? throw new System.ArgumentNullException(nameof(appFixture));

            Fixture = new Fixture();
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }


        protected HttpClient CreateClient()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = _appFixture.ClientOptions.BaseAddress,
                HandleCookies = true
            };

            return _appFixture.CreateClient(options);
        }

        protected AppDbContext GetDbContext()
        {
            var scope = _appFixture.Services.CreateScope();
            return scope.ServiceProvider.GetService<AppDbContext>();
        }
    }
}
