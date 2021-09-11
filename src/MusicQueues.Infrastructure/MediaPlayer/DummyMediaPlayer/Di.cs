using Microsoft.Extensions.DependencyInjection;
using MusicQueues.Application.Common.Interfaces;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public static class Di
    {
        public static IServiceCollection AddDummyMediaPlayer(this IServiceCollection services)
        {
            services.AddTransient<IMediaPlayer, DummyMediaPlayer>();
            return services;
        }
    }
}