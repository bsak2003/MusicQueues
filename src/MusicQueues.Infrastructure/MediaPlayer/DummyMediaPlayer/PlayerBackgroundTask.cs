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
            
            if (element == null) return;
            
            _logger.LogInformation($"Playing element {element.Id} (${element.Title}) from queue {queueId} for ~10s");
            await Task.Delay(10000, cancellationToken);
            
            if (cancellationToken.IsCancellationRequested) return;
            
            await _mediator.Send(new DeleteElement(queueId, element.Id), cancellationToken);
            BackgroundJob.Enqueue<PlayerBackgroundTask>(x => x.Play(queueId, cancellationToken));
        }
    }
}