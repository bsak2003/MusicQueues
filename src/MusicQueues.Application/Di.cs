using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MusicQueues.Application
{
    public static class Di
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}