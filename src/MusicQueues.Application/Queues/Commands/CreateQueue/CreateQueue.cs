using System;
using MediatR;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Queues.Commands.CreateQueue
{
    public class CreateQueue : IRequest<Guid>
    {
        public Platform Platform { get; set; }
    }
}