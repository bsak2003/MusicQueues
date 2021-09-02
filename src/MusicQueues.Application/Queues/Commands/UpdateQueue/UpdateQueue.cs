using System;
using MediatR;

namespace MusicQueues.Application.Queues.Commands.UpdateQueue
{
    public class UpdateQueue : IRequest
    {
        public UpdateQueue(Guid id, string title = "", string description = "")
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
    }
}