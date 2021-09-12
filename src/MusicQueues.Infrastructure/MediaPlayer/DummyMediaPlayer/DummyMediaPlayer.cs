using System;
using System.Linq;
using System.Threading;
using Hangfire;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
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
            BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, CancellationToken.None));
        }

        public void StopPlayback(Guid queueId)
        {
            _logger.LogInformation($"Stopped playback of queue {queueId}");
            
            var api = JobStorage.Current.GetMonitoringApi();

            var processingJobs = api.ProcessingJobs(0, int.MaxValue)
                .Where(x => x.Value.Job.Type == typeof(PlayerBackgroundTask))
                .Where(x => x.Value.Job.Args.Contains(queueId))
                .Select(x => x.Key);

            var enqueuedJobs = api.EnqueuedJobs("default", 0, int.MaxValue)
                .Where(x => x.Value.Job.Type == typeof(PlayerBackgroundTask))
                .Where(x => x.Value.Job.Args.Contains(queueId))
                .Select(x => x.Key);

            foreach (var job in Enumerable.Concat(processingJobs, enqueuedJobs))
            {
                BackgroundJob.Delete(job);
            }
        }
    }
}