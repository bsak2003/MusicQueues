using System;
using System.Collections.Generic;
using System.Threading;
using Hangfire;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class DummyMediaPlayer : IMediaPlayer
    {
        public static Dictionary<Guid, string> JobIds { get; } = new Dictionary<Guid, string>();
        private readonly ILogger<DummyMediaPlayer> _logger;
        public DummyMediaPlayer(ILogger<DummyMediaPlayer> logger)
        {
            _logger = logger;
        }

        public Platform Platform { get; } = Platform.Dummy;

        public void QueueCreated(Queue queue)
        {
            _logger.LogInformation($"Registered queue {queue.Id}");
        }

        public void QueueRemoved(Guid queueId)
        {
            _logger.LogInformation($"Unregistered queue {queueId}");
        }

        public void SongAdded(Guid queueId, QueueElement element)
        {
            _logger.LogInformation($"Added song {element.Id} to queue {queueId}");
        }

        public void SongMoved(Guid queueId, Guid songId, int position)
        {
            _logger.LogInformation($"Moved song {songId} in queue {queueId} into position {position}");
        }

        public void SongRemoved(Guid queueId, Guid songId)
        {
            _logger.LogInformation($"Removed song {songId} from {queueId}");
        }

        public void RefreshQueue(Queue queue)
        {
            _logger.LogInformation($"Refreshed queue {queue.Id}");
        }

        public void StartPlayback(Guid queueId)
        {
            _logger.LogInformation($"Started playback of queue {queueId}");
            var jobId = BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, CancellationToken.None));
            JobIds.Add(queueId, jobId);
        }

        public void StopPlayback(Guid queueId)
        {
            BackgroundJob.Delete(JobIds[queueId]);
            JobIds.Remove(queueId);
        }
    }
}