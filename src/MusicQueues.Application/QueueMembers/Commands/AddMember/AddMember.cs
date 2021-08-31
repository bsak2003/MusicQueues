using System;
using MediatR;

namespace MusicQueues.Application.QueueMembers.Commands.AddMember
{
    public class AddMember : IRequest
    {
        public AddMember(Guid queueId)
        {
            QueueId = queueId;
        }

        public Guid QueueId { get; }
    }
}