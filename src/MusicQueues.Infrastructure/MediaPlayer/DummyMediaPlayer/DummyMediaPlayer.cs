using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Hangfire;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class DummyMediaPlayer : IMediaPlayer
    {
        public DummyMediaPlayer(ILogger<DummyMediaPlayer> logger, IRepository<Queue> repository)
        {
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