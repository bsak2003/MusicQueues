using Microsoft.Extensions.DependencyInjection;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Api.Services
{
    public static class Di
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}