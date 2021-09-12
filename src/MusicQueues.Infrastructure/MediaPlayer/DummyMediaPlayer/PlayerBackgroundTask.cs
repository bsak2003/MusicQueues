using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using MusicQueues.Application.QueueElements.Commands.DeleteElement;
using MusicQueues.Application.Queues.Queries.ReadQueueById;

namespace MusicQueues.Infrastructure.MediaPlayer.DummyMediaPlayer
{
    public class PlayerBackgroundTask
    {
        private readonly IMediator _mediator;
        private readonly Random _rng;
        private readonly ILogger<PlayerBackgroundTask> _logger;
        
        public PlayerBackgroundTask(IMediator mediator, ILogger<PlayerBackgroundTask> logger)
        {
            _mediator = mediator;
            _rng = new Random();
            _logger = logger;
        }

        public async Task Play(Guid queueId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var queue = await _mediator.Send(new ReadQueueById(queueId), cancellationToken);
            var element = queue.Elements.FirstOrDefault();
            
            if (element == null) return;
            
            _logger.LogInformation($"Playing element {element.Id} from queue {queueId}");
            await Task.Delay(_rng.Next(10000, 60000), cancellationToken);
            
            if (cancellationToken.IsCancellationRequested) return;
            
            await _mediator.Send(new DeleteElement(queueId, element.Id), cancellationToken);
            var jobId = BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, cancellationToken));
            DummyMediaPlayer.JobIds[queueId] = jobId;
        }
    }
}