using System;
using MusicQueues.Domain.Entities;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Application.Queues.Queries.Common
{
    public class QueueMemberDto
    {
        public QueueMemberDto(QueueMember member)
        {
            Id = member.Id;
            Reference = member.Reference;
            Role = member.Role;
        }
        
        public Guid Id { get; }
        public Guid Reference { get; }
        public MemberRole Role { get; private set; }
    }
}