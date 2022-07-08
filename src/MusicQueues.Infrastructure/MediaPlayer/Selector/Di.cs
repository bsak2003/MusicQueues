using Microsoft.Extensions.DependencyInjection;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;

namespace MusicQueues.Infrastructure.MediaPlayer.Selector
{
    public static class Di
    {
        public static IServiceCollection AddMediaPlayerSelector(this IServiceCollection services)
        {
            services.AddTransient<IMediaPlayerSelector, MediaPlayerSelector>();
            return services;
        }
    }
}