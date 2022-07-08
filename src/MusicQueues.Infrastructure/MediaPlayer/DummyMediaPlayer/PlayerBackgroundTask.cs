using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.Common.Interfaces;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class PlayerBackgroundTask
    {
        private readonly IRepository<Queue> _queueRepository;
        private readonly ILogger<PlayerBackgroundTask> _logger;
        
        public PlayerBackgroundTask(IRepository<Queue> queueRepository, ILogger<PlayerBackgroundTask> logger)
        {
            _queueRepository = queueRepository;
            _logger = logger;
        }

        public async Task Play(Guid queueId, CancellationToken cancellationToken)
        {
            // pending refactor anyway I guess
            var queue = await _queueRepository.ReadById(queueId);
            var element = queue.Elements.FirstOrDefault();

            if (element is null)
            {
                queue.UpdateStatus(Status.Stopped);
                await _queueRepository.Update(queue);
                return;
            }
            
            _logger.LogInformation($"Playing element {element.Id} ({element.Title}) from queue {queueId} for ~30s");
            await Task.Delay(30000, cancellationToken);
            
            if (cancellationToken.IsCancellationRequested) return;
            
            queue.RemoveElement(element);
            await _queueRepository.Update(queue);

            BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, cancellationToken));
        }

        public static void Stop(Guid queueId)
        {
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