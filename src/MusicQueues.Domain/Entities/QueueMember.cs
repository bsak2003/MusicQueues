using System;
using MusicQueues.Domain.Enums;

namespace MusicQueues.Domain.Entities
{
    public class QueueMember
    {
        private MemberRole _role;

        public QueueMember(string reference, MemberRole role)
        {
            Id = Guid.NewGuid();
            Reference = reference;
            _role = role;
        }
        
        public Guid Id { get; }
        public string Reference { get; }
        public MemberRole Role => _role;
        
        public void UpdateRole(MemberRole role)
        {
            _role = role;
        }
    }
}