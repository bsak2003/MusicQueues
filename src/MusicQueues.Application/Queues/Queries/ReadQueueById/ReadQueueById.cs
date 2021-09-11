using System;
using MediatR;
using MusicQueues.Application.Queues.Queries.Common;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Queries.ReadQueueById
{
    public class ReadQueueById : IRequest<QueueDto>
    {
        public ReadQueueById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}