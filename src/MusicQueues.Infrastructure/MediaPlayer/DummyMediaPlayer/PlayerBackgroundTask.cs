using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.QueueElements.Commands.DeleteElement;
using MusicQueues.Application.Queues.Commands.StopQueue;
using MusicQueues.Application.Queues.Queries.ReadQueueById;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class PlayerBackgroundTask
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlayerBackgroundTask> _logger;
        
        public PlayerBackgroundTask(IMediator mediator, ILogger<PlayerBackgroundTask> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Play(Guid queueId, CancellationToken cancellationToken)
        {
            var queue = await _mediator.Send(new ReadQueueById(queueId), cancellationToken);
            var element = queue.Elements.FirstOrDefault();

            if (element == null)
            {
                await _mediator.Send(new StopQueue(queueId), cancellationToken);
                return;
            }
            
            _logger.LogInformation($"Playing element {element.Id} ({element.Title}) from queue {queueId} for ~10s");
            await Task.Delay(10000, cancellationToken);
            
            if (cancellationToken.IsCancellationRequested) return;
            
            await _mediator.Send(new DeleteElement(queueId, element.Id), cancellationToken);
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