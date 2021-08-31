using System;
using MediatR;

namespace MusicQueues.Application.QueueElements.Commands.DeleteElement
{
    public class DeleteElement : IRequest
    {
        public DeleteElement(Guid queueId, Guid elementId)
        {
            QueueId = queueId;
            ElementId = elementId;
        }

        public Guid QueueId { get; }
        public Guid ElementId { get; }
    }
}