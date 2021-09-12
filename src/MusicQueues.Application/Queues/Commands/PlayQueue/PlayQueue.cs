using System;
using MediatR;

namespace MusicQueues.Application.Queues.Commands.PlayQueue
{
    public class PlayQueue : IRequest
    {
        public PlayQueue(Guid queueId)
        {
            QueueId = queueId;
        }
        
        public Guid QueueId { get; }
    }
}