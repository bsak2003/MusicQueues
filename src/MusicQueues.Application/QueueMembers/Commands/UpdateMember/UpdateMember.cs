using System;
using MediatR;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.QueueMembers.Commands.UpdateMember
{
    public class UpdateMember : IRequest
    {
        public UpdateMember(Guid queueId, Guid memberId, MemberRole newRole)
        {
            QueueId = queueId;
            MemberId = memberId;
            NewRole = newRole;
        }

        public Guid QueueId { get; }
        public Guid MemberId { get; }
        public MemberRole NewRole { get; }
    }
}