using System;
using MediatR;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Queues.Commands.CreateQueue
{
    public class CreateQueue : IRequest<Guid>
    {
        public CreateQueue(Platform platform, string title, string description)
        {
            Platform = platform;
            Title = title;
            Description = description;
        }
        public Platform Platform { get; }
        public string Title { get; }
        public string Description { get; }
    }
}