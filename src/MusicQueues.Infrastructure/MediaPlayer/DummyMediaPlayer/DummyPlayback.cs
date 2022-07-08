using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Application.Common.Interfaces.MediaPlayers;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class DummyPlayback : IMediaPlayback
    {
        private readonly ILogger<DummyMediaPlayer> _logger;

        public DummyPlayback(ILogger<DummyMediaPlayer> logger)
        {
            _logger = logger;
        }
        
        public Task Start(Guid queueId)
        {
            BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, CancellationToken.None));
            _logger.LogInformation($"Started playback of queue {queueId}");

            return Task.CompletedTask;
        }

        public Task Stop(Guid queueId)
        {
            PlayerBackgroundTask.Stop(queueId);
            _logger.LogInformation($"Stopped playback of queue {queueId}");

            return Task.CompletedTask;
        }
    }
}