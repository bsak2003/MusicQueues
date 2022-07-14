using System;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Enums;
using MusicQueues.Infrastructure.MediaPlayer.Spotify.MediaPlayer;

namespace MusicQueues.Infrastructure.MediaPlayer.Spotify;

public class SpotifyMediaPlayer : IMediaPlayer
{
    public SpotifyMediaPlayer(SpotifySetup player, SpotifyPlayback playback, SpotifyControls controls, SpotifyRefresh refresh)
    {
        Player = player;
        Playback = playback;
        Controls = controls;
        Refresh = refresh;
    }

    public Platform Platform { get; } = Platform.Spotify;
    public IPlayerSetup Player { get; }
    public IMediaPlayback Playback { get; }
    public IPlayerControls Controls { get; }
    public IMediaRefresh Refresh { get; }
}