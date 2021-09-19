using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    // TODO integrate & test
    public class DummyMediaPlayer : IMediaPlayer
    {
        public DummyMediaPlayer(ILogger<DummyMediaPlayer> logger, IRepository<Queue> repository)
        {
            // TODO Internal Dependency Injection (should we use .NET Dependency Injection or just construct components in constructor?) 
            // TODO Generic/specific logger? (now we pass ILogger<DummyMediaPlayer> everywhere but PlayerBackgroundTask) 
            // TODO DummyMediaPlayer organization (pack components into folder or integrate them into single class - since it's only reference)
            Player = new DummySetup(logger, repository);
            Playback = new DummyPlayback(logger, repository);
            Controls = new DummyControls(logger, repository);
            Refresh = new DummyRefresh(logger, repository);
        }

        public Platform Platform { get; } = Platform.Dummy;
        public IPlayerSetup Player { get; }
        public IMediaPlayback Playback { get; }
        public IPlayerControls Controls { get; }
        public IMediaRefresh Refresh { get; }
    }
}