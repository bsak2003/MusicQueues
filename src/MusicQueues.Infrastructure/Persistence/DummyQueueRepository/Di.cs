using Microsoft.Extensions.DependencyInjection;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Infrastructure.Persistence.DummyQueueRepository
{
    public static class Di
    {
        public static IServiceCollection AddDummyQueueRepository(this IServiceCollection services)
        {
            services.AddSingleton<IRepository<Queue>, DummyQueueRepository>();
            return services;
        }
    }
}