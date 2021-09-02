using System;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Domain.Entities
{
    public class QueueMember
    {
        public QueueMember(Guid reference, MemberRole role)
        {
            Id = Guid.NewGuid();
            Reference = reference;
            Role = role;
        }
        
        public Guid Id { get; }
        public Guid Reference { get; }
        public MemberRole Role { get; private set; }

        public void UpdateRole(MemberRole role)
        {
            Role = role;
        }
    }
}