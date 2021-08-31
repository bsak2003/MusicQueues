using Microsoft.Extensions.DependencyInjection;
using MusicQueues.Infrastructure.Persistence.DummyQueueRepository;

namespace MusicQueues.Infrastructure
{
    public static class Di
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDummyQueueRepository();
            return services;
        }
    }
}