using Microsoft.Extensions.DependencyInjection;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Persistence;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.Services;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify;

public static class Di
{
    public static IServiceCollection AddSpotify(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<SpotifyQueue>, SpotifyQueueRepository>();
        
        services.AddTransient<SpotifyControls>();
        services.AddTransient<SpotifyPlayback>();
        services.AddTransient<SpotifyRefresh>();
        services.AddTransient<SpotifySetup>();
        
        services.AddTransient<IMediaPlayer, SpotifyMediaPlayer>();

        return services;
    }
}