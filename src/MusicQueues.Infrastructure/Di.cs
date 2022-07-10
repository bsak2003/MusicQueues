using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MusicQueues.Infrastructure.Hangfire;
using MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer;
using MusicQueues.Infrastructure.MediaPlayer.Selector;
using MusicQueues.Infrastructure.MediaPlayer.Spotify;
using MusicQueues.Infrastructure.Persistence.DummyQueueRepository;

namespace MusicQueues.Infrastructure
{
    public static class Di
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDummyQueueRepository();
            services.AddDummyMediaPlayer();
            services.AddSpotify();
            services.AddMediaPlayerSelector();
            services.AddHangfireTasks();
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseHangfireTasks();
            return app;
        }
    }
}