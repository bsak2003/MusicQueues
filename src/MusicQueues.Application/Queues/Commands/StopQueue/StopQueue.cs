using System;
using MediatR;

namespace MusicQueues.Application.Queues.Commands.StopQueue
{
    public class StopQueue : IRequest
    {
        public StopQueue(Guid queueId)
        {
            QueueId = queueId;
        }

        public Guid QueueId { get; }
    }
}