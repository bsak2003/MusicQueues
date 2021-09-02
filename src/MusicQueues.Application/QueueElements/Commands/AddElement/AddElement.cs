using System;
using MediatR;

namespace MusicQueues.Application.QueueElements.Commands.AddElement
{
    public class AddElement : IRequest<Guid>
    {
        public AddElement(Guid queueId, string reference, string title)
        {
            QueueId = queueId;
            Reference = reference;
            Title = title;
        }

        public Guid QueueId { get; set; }
        public string Reference { get; }
        public string Title { get; }
    }
}