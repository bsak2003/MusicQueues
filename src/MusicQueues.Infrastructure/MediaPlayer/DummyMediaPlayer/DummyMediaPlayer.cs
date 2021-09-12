using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ILogger<DummyMediaPlayer> _logger;
        public DummyMediaPlayer(ILogger<DummyMediaPlayer> logger)
        {
            _logger = logger;
        }

        public Platform Platform { get; } = Platform.Dummy;
        
        public void StartPlayback(Guid queueId)
        {
            _logger.LogInformation($"Started playback of queue {queueId}");
            var jobId = BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, CancellationToken.None));
        }

        public void StopPlayback(Guid queueId)
        {
            var api = JobStorage.Current.GetMonitoringApi();
            var jobId = api.ProcessingJobs(0, int.MaxValue)
                .Where(x => x.Value.Job.Type == typeof(PlayerBackgroundTask))
                .Where(x => x.Value.Job.Args.Contains(queueId))
                .Select(x => x.Key)
                .First();
            BackgroundJob.Delete(jobId);
        }
    }
}