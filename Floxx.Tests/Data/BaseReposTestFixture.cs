using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floxx.Core.Interfaces;
using Floxx.Infrastructure.Contexts;
using Floxx.Infrastructure.Repositories;
using Floxx.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Floxx.Tests.Data
{
    public abstract class BaseRepoTestFixture
    {
        protected FloxxContext dbContext;

        protected static DbContextOptions<FloxxContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<FloxxContext>();
            builder.UseInMemoryDatabase("FloxxTests")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
