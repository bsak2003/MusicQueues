using Microsoft.Extensions.DependencyInjection;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Api.Services
{
    public static class Di
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            // To use with Identity Framework
            // services.AddTransient<ICurrentUserService, CurrentIdentityUserService>();
            
            // To use with Anonymous User Service
            services.AddTransient<ICurrentUserService, CurrentAnonymousUserService>();
            
            return services;
        }
    }
}