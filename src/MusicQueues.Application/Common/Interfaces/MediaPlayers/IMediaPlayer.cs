using System;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Common.Interfaces.MediaPlayers
{
    public interface IMediaPlayer
    {
        // TODO async void vs. async Task
        // TODO Queue vs queueId
        public Platform Platform { get; }
        public IPlayerSetup Player { get; }
        public IMediaPlayback Playback { get; }
        public IPlayerControls Controls { get; }
        public IMediaRefresh Refresh { get; }
    }
}