using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MusicQueues.Infrastructure.Hangfire
{
    public static class Di
    {
        public static IServiceCollection AddHangfireTasks(this IServiceCollection services)
        {
            services.AddHangfire(conf =>
            {
                conf.UseMemoryStorage();
            });
            
            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseHangfireTasks(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/tasks");
            return app;
        }
    }
}