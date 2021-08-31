using System;
using MediatR;
using MusicQueues.Domain.Entities;

namespace MusicQueues.Application.Queues.Queries.ReadQueueById
{
    public class ReadQueueById : IRequest<Queue>
    {
        public ReadQueueById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}