using System;
using MediatR;

namespace MusicQueues.Application.Queues.Commands.DeleteQueue
{
    public class DeleteQueue : IRequest
    {
        public DeleteQueue(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}