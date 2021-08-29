using System;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Domain.Entities
{
    public class QueueMember
    {
        private MemberRole _role;

        public QueueMember(MemberRole role)
        {
            Id = Guid.NewGuid();
            _role = role;
        }
        
        public Guid Id { get; }
        public MemberRole Role => _role;
        
        public void UpdateRole(MemberRole role)
        {
            _role = role;
        }
    }
}