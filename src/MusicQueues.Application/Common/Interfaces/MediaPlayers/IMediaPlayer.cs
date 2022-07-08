using System;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IMediaPlayer
    {
        public Platform Platform { get; }
        public IPlayerSetup Player { get; }
        public IMediaPlayback Playback { get; }
        public IPlayerControls Controls { get; }
        public IMediaRefresh Refresh { get; }
    }
}