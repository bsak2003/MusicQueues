using MediatR;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Commands.UpdateQueue
{
    public class UpdateQueue : IRequest
    {
        public UpdateQueue(Queue queue)
        {
            Queue = queue;
        }

        public Queue Queue { get; } 
    }
}