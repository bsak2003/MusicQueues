using System;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Domain.Entities
{
    public class QueueMember
    {
        public QueueMember(string reference, MemberRole role)
        {
            Id = Guid.NewGuid();
            Reference = reference;
            Role = role;
        }
        
        public Guid Id { get; }
        public string Reference { get; }
        public MemberRole Role { get; private set; }

        public void UpdateRole(MemberRole role)
        {
            Role = role;
        }
    }
}