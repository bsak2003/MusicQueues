using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    // TODO integrate & test => I guess we should only test every endpoint by hand, and then head to Spotify integration
    // but at first, we need to create endpoints to some of MediaPlayer methods (such as Pause/Unpause, etc) and alter
    // AddQueueElement behavior to include skipping;
    public class DummyMediaPlayer : IMediaPlayer
    {
        public DummyMediaPlayer(ILogger<DummyMediaPlayer> logger, IRepository<Queue> repository)
        {
            /* DI and organization (apart from IMediaPlayer) is purely internal matter; here we simply implement
             every interface separately and instantiate them in a constructor, but a more complex solution may use
             1st or 3rd party dependency injection scheme; logging is advised to use main class as template, but it's
             also an internal matter; in a proper IMediaPlayer probably packing in folders will be more suitable, 
             but for dummy a bunch of loose files is okay */
            Player = new DummySetup(logger, repository);
            Playback = new DummyPlayback(logger);
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