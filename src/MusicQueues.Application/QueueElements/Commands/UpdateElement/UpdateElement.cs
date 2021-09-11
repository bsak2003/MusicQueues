using System;
using MediatR;

namespace MusicQueues.Application.QueueElements.Commands.UpdateElement
{
    public class UpdateElement : IRequest
    {
        public UpdateElement(Guid queueId, Guid elementId, int newPosition)
        {
            QueueId = queueId;
            ElementId = elementId;
            NewPosition = newPosition;
        }

        public Guid QueueId { get; }
        public Guid ElementId { get; }
        public int NewPosition { get; }
    }
}