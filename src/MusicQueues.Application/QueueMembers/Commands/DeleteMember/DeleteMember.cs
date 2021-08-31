using System;
using MediatR;

namespace MusicQueues.Application.QueueMembers.Commands.DeleteMember
{
    public class DeleteMember : IRequest
    {
        public DeleteMember(Guid queueId, Guid memberId)
        {
            QueueId = queueId;
            MemberId = memberId;
        }

        public Guid QueueId { get; }
        public Guid MemberId { get; }
    }
}