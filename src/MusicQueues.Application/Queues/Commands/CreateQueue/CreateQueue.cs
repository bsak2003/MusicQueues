using System;
using MediatR;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Queues.Commands.CreateQueue
{
    public class CreateQueue : IRequest<Guid>
    {
        public CreateQueue(Platform platform)
        {
            Platform = platform;
        }
        public Platform Platform { get; }
    }
}